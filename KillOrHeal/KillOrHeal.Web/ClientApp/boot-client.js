import './css/site.css';
import './signalr-client-1.0.0-alpha2-final';
import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { AppContainer } from 'react-hot-loader';
import GameContainer from './containers/GameContainer';

const renderApp = ()=>{
  ReactDOM.render(
      <AppContainer>
          <GameContainer logSize={10} />
    </AppContainer>, document.getElementById('react-app'));
};

renderApp();

if (module.hot) {
  module.hot.accept();
}
