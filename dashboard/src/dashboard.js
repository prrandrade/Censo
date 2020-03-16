import React from "react";
import * as signalR from '@microsoft/signalr';
import { Property } from './property'

export class Dashboard extends React.Component {

    state = {
        hub: null,
        regions: null,
        genders: null,
        schoolings: null,
        ethnicities: null,
    }
    componentDidMount() {
        const hub =  new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:8080/hub")
            .withAutomaticReconnect([5000])
            .configureLogging(signalR.LogLevel.Information)
            .build();

        this.setState({hub}, () => {
            this.state.hub.start()
                .then(() => {
                    this.state.hub.on("UpdateDashboard", (info) => {
                        this.setState({...this.state,
                            regions: info.regions,
                            genders: info.genders,
                            schoolings: info.schoolings,
                            ethnicities: info.ethnicities });
                    });

                })
                .catch(err => console.log(`SignalR Error: ${err}`));
        });
    }

    render() {
        return (
            <div>
                <nav className="navbar navbar-light bg-light">
                    <span className="navbar-brand mb-0 h1">Dashboard Censo</span>
                </nav>
                <div>
                    <br/>
                    <Property titulo="Regiões" valores={this.state.regions}/>
                    <br/>
                    <Property titulo="Gêneros" valores={this.state.genders}/>
                    <br/>
                    <Property titulo="Escolaridades" valores={this.state.schoolings}/>
                    <br/>
                    <Property titulo="Regiões" valores={this.state.ethnicities}/>
                    <br/>
                </div>
            </div>
        )
    }
}