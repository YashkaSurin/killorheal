import * as React from 'react';

class Greetings extends React.Component {
    
    render() {
		const { player } = this.props;
        return (
            <div>
                <p>Welcome to Kill Or Heal!</p>
                <p>You are Player {player.playerId}.</p>
                <p>Your level is {player.level} and your health is {player.health}.</p>
				<p>Your combat type is {player.combatType}.</p>
				<p>You are a member of: {player.factions.map(f => 'Faction ' + f).join(', ')}.</p>
                <p>Press left mouse button to attack and right mouse button to heal.</p>
            </div>

        )
    }
}

export default Greetings;