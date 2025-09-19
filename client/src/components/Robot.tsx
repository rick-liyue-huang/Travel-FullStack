import React from "react";

interface RobotProps {id: number, name: string, email: string}

const Robot: React.FC<RobotProps> = (props) => {

    const { id, name, email } = props;

    return (
        <li>
            <img src={`https://robohash.org/${id}`} alt="robot" />
            <h2>{name}</h2>
            <p>{email}</p>
        </li>
    )
}

export default Robot;