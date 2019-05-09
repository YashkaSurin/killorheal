import * as React from 'react';

class GameBoard extends React.Component {
    onContextMenu = (e, x, y) => {
        e.preventDefault();
        this.props.onCellRightClick(x, y);
    }

    getCellContentType = (x ,y) => {
        const player = this.props.game.playerPositions[x] ? this.props.game.playerPositions[x][y] : null;
		if (player) {
			const currentPlayer = this.props.currentPlayer;
			if (player.playerId === currentPlayer.playerId) {
				return 'current-player';
			}

			if (player.factions.filter(f => currentPlayer.factions.includes(f)).length > 0) {
				return 'ally';
			}

			return 'enemy';
		}


        return 'empty';
    }

    render() {
        const { game, onCellClick } = this.props;
        let rows = [];
        for (let i = 0; i < game.mapSize; i++) {
            let cells = [];
            for (let j = 0; j < game.mapSize; j++) {
                var cellContentType = this.getCellContentType(i, j);
                var cellContent = cellContentType === 'empty' ? '' : this.props.game.playerPositions[i][j].health; 
                cells.push(<td key={j} onClick={() => onCellClick(i, j)} onContextMenu={e => this.onContextMenu(e, i, j)} className={cellContentType}>{cellContent}</td>);
            }
            rows.push(<tr key={i}>{cells}</tr>)
        }

        return (
            <div className="game-board">
                <table>
                    <tbody>
                        {rows}
                    </tbody>
                </table>
            </div>
        )
    }
}

export default GameBoard;