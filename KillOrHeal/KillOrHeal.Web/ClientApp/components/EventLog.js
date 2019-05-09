import * as React from 'react';
import dateFormat from 'dateformat';

class EventLog extends React.Component {

    generateKey = () => Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);

    render() {
        return (
            <div>
                {this.props.log.map(entry => 
                    <div key={this.generateKey()} className={entry.type === 'Error' ? 'error' : ''}>{dateFormat(entry.timestamp, 'yyyy-mm-dd HH:mm:ss')}: {entry.message}</div>
                )}
            </div>
            
        )
    }
}

export default EventLog;