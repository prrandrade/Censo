import React from "react";

export const Property = (props) =>
    <div>
        <h3>{props.titulo}</h3>
        <ul>
            {console.log(props.valores)}
            { (props.valores != null) && props.valores.map((v, _) => <li>{v.value}: {v.count} respostas</li>) }
        </ul>
    </div>