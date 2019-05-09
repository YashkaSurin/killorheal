import * as React from 'react';
import axios from 'axios';
import GameBoard from '../components/GameBoard';
import EventLog from '../components/EventLog';
import FactionSelector from '../components/FactionSelector';
import Greetings from '../components/Greetings';

class GameContainer extends React.Component {
    constructor() {
        super();
        this.state = {
            game: {
				mapSize: window.killOrHeal.mapSize,
				factions: [],
                players: [],
                playerPositions: {}
            },
            me: {
	            factions: []
            },
            log: []
        };
    }

    componentDidMount() {
        this.initSignalR();
        this.fetchFullState(() => {
            this.joinGame();            
        });        
    }

    fetchFullState = (onStateReceived) => {
        axios.get('/api/game')
            .then(response => {
                let game = response.data;
                game.playerPositions = this.populatePlayerPositions(game.players);
                this.setState({ game });
                if (typeof onStateReceived === 'function') {
                    onStateReceived();
                }
            });
    };

    populatePlayerPositions = players => {
        const positions = {};
        players.forEach(p => {
            positions[p.coordinates.x] = positions[p.coordinates.x] || {};
            positions[p.coordinates.x][p.coordinates.y] = p;
        });

        return positions;
    }

    joinGame = () => {
        if (this.state.me.id) {
	        alert('You are already in the game.');
        }

        axios.post('/api/players')
            .then(response => {
                const game = this.state.game;
                this.addPlayer(response.data, game);
                this.setState({
                    game,
                    me: response.data
                });
			})
			.catch(error => {
		        alert(error.response.data.message || 'An error happened. Please contact administrator.');
	        });
    }

    initSignalR = () => {        
        const transportType = window.signalR.TransportType.WebSockets;
        const logger = new window.signalR.ConsoleLogger(signalR.LogLevel.Information);
        const eventsHub = new window.signalR.HttpConnection(`/events`, { transport: transportType, logger: logger });
        const eventsConnection = new window.signalR.HubConnection(eventsHub, logger);

        eventsConnection.onClosed = e => {
            console.log('connection closed');
        };

        eventsConnection.on('broadcast', (e) => {
            const message = JSON.parse(e);
            let game = this.state.game;
            if (e.to && e.to !== game.me.playerId) {
                return;
            }

            switch (message.type) {
                case 'NewPlayerJoined':
                    this.addPlayer(message.player, game);
                    this.setState({ game });
                    break;
                case 'PlayerWasDamaged':
                case 'PlayerWasHealed':                    
                    let filteredPlayers = game.players.filter(p => p.playerId === message.playerId);
                    if (filteredPlayers.length > 0) {
                        filteredPlayers[0].health = message.health;
                        this.setState({ game });
                    }
					break;
				case 'PlayerJoinedFaction':
					let joinedPlayers = game.players.filter(p => p.playerId === message.player.playerId);
					if (joinedPlayers.length > 0) {
						joinedPlayers[0].factions.push(message.factionId);
						this.setState({ game });
					}
					break;
				case 'PlayerLeftFaction':
					let leftPlayers = game.players.filter(p => p.playerId === message.player.playerId);
					if (leftPlayers.length > 0) {
						leftPlayers[0].factions = leftPlayers[0].factions.filter(f => f !== message.factionId);
						this.setState({ game });
					}
					break;
            }

            this.addEventToLog(message);
        });

        eventsConnection.start().catch(err => {
            console.log('connection error');
        });
    }

    executeActionOnPlayer = (x, y, action) => {
        const player = this.findPlayerOnMap(x, y);
        if (!player) {
            return;
        }

        axios.put('/api/players/' + player.playerId + '/' + action, {}, {
            headers: {
                authorization: this.state.me.playerId
            }
        }).then(_ => { });
    }

    onBoardCellClick = (x, y) => {
        this.executeActionOnPlayer(x, y, 'attack');
    }

    onBoardCellRightClick = (x, y) => {
        this.executeActionOnPlayer(x, y, 'heal');
	}

	onFactionClick = f => {
		const action = this.state.me.factions.includes(f) ? 'leave' : 'join';
		axios.put('/api/factions/' + f + '/' + action, {}, {
			headers: {
				authorization: this.state.me.playerId
			}
		}).then(_ => { });
	}

    findPlayerOnMap = (x, y) => {
        const positions = this.state.game.playerPositions;
        if (positions[x] === undefined || positions[x][y] === undefined) {
            return null;
        }

        return positions[x][y];
    }

    addPlayer = (player, game) => {
        if (game.players.filter(p => p.playerId === player.playerId).length > 0) {
            return;
        }
        game.players.push(player);
        game.playerPositions[player.coordinates.x] = game.playerPositions[player.coordinates.x] || {};
        game.playerPositions[player.coordinates.x][player.coordinates.y] = player;
    }

    addEventToLog = message => {
        let log = this.state.log;
        log.push({
            type: message.type,
            message: message.message,
            timestamp: new Date(message.timestamp)
        });
        log.sort((a, b) => b.timestamp - a.timestamp);
        if (log.length > this.props.logSize) {
            log = log.slice(0, this.props.logSize - 1);
        }
        this.setState({ log });
    }

    render() {
        const { game, me, log } = this.state;

        return (
            <div>
                {me &&
                    <Greetings player={me} />
				}
				{game.factions.length > 0 &&
					<FactionSelector allFactions={game.factions} playerFactions={me.factions} onFactionClick={this.onFactionClick} />
				}
                <div>
                    <div style={{ float: 'left', width: '50%' }}>
                        <GameBoard game={game} onCellClick={this.onBoardCellClick} onCellRightClick={this.onBoardCellRightClick} currentPlayer={me} />
                    </div>
                    <div>
                        <EventLog log={log} />
                    </div>
                </div>
            </div>
            )
    }
}

export default GameContainer;