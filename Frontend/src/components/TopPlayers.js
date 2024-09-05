import React, { useEffect, useState } from "react";
import { TopPlayers } from "../Services/APIService";
import '../styles/Table.css';

const TopPlays = () => {
  const [q3, setq3] = useState([]);

  useEffect(() => {
    const getDataQ3 = async () => {
      let data = await TopPlayers();
      if (Array.isArray(data)) {
        setq3(data);
      } else {
        console.error("Invalid response from GetDataQ4");
      }
    };
    getDataQ3();
  }, []);

  return (
    <>
      <div className="container">
  <div className="row justify-content-center">
    <table className="table table-striped table-bordered">
      <thead>
        <tr>
          <th>Player ID</th>
          <th>Player Name</th>
          <th>Matches Played</th>
        </tr>
      </thead>
      <tbody>
        {q3.map((p) => (
          <tr key={p.playerName}>
            <td>{p.playerId}</td>
            <td>{p.playerName}</td>
            <td>{p.matchesPlayed}</td>
          </tr>
        ))}
      </tbody>
    </table>
  </div>
</div>
    </>
  );
};

export default TopPlays;
