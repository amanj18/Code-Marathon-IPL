import React, { useEffect, useState } from "react";
import { MatchDetails } from "../Services/APIService";
import '../styles/Table.css';

const Details = () => {
  const [q2, setq2] = useState([]);

  useEffect(() => {
    const getData = async () => {
      let data = await MatchDetails();
      console.log(data);
      if (Array.isArray(data)) {
        setq2(data);
        
      } else {
        console.error("Invalid response from GetDataQ4");
      }
    };
    getData();
  }, []);

  return (
    <>
 <div className="container">
  <div className="row justify-content-center">
    <table className="table table-striped table-bordered">
      <thead>
        <tr>
        <th>Match ID</th>
          <th>Team 1</th>
          <th>Team 2</th>
          <th>Venue</th>
          <th>Match Date</th>
          <th>Fan Enagagement</th>
        </tr>
      </thead>
      <tbody>
        {q2.map((p) => (
             <tr>
             <td>{p.matchId}</td>
             <td>{p.team1Name}</td>
             <td>{p.team2Name}</td>
             <td>{p.venue}</td>
             <td>{p.matchDate}</td>
             <td>{p.fanEngagementCount}</td>
           </tr>
        ))}
      </tbody>
    </table>
  </div>
</div>

    </>
  );
};

export default Details;
