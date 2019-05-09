import * as React from 'react';

class FactionSelector extends React.Component {
	render() {
		const { allFactions, playerFactions, onFactionClick } = this.props;

		return (
			<div className="faction-selector">
				{allFactions.map(f =>
					<div key={f}>
						<input type="checkbox" checked={playerFactions.includes(f)} id={"chk-faction-" + f}
							onClick={() => onFactionClick(f)} />
						<label htmlFor={"chk-faction-" + f}>Faction {f}</label>
					</div>
				)}
			</div>
		)
	}
}

export default FactionSelector;