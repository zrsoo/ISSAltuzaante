import React from 'react'
import HttpRequestController from '../../controller/HttpRequestController';
import AuthenticationController from '../../controllers/AuthenticationController';

export default class Logout extends React.Component {
    handleSubmit = e => {
        e.preventDefault();
    };

    render() {
        //console.log("Pagina logout");
        //TODO, implement logout on back-end (reomove token)
        //AuthenticationController.logout();
        localStorage.clear();
        //this.props.history.push("/home");
        return (
            <div>
                <h2>Bye bye</h2>
            </div>
        );
    }
}