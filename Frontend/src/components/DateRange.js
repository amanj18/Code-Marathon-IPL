import React, { useEffect, useState } from "react";
import { DateRange } from "../Services/APIService";

const Range = () => {

  const [q4, setq4] = useState([]);
  const [start, setStart] = useState("");
  const [end, setEnd] = useState("");
  const [input1, setInput1] = useState("");
  const [input2, setInput2] = useState("");

  useEffect(() => {
    const getDataQ4 = async () => {
      let data = await DateRange(start,end);
      if (Array.isArray(data)) {
        setq4(data);
      } else {
        console.error("Invalid response from GetDataQ4");
      }
    };
    getDataQ4();
  }, [start, end]); // Changed input to year

  const onChange1 = (e) => {
    setInput1(e.target.value);
  };

  const onChange2 = (e) => {
    setInput2(e.target.value);
  };

  const onSubmit = (e) => {
    e.preventDefault();
    setStart(input1);
    setEnd(input2);
  };

  return (
    <div className="container mt-5">
      <form className="form-group" onSubmit={onSubmit}>
        <div className="form-group">
          <label htmlFor="start">Start date</label>
          <input
            type="date"
            className="form-control"
            id="start"
            value={input1}
            onChange={onChange1}
          />
        </div>
        <div className="form-group">
          <label htmlFor="end">End date</label>
          <input
            type="date"
            className="form-control"
            id="end"
            value={input2}
            onChange={onChange2}
          />
        </div>
        <button type="submit" className="btn btn-primary">
          Get detail
        </button>
      </form>
      <h3 className="text-center mt-3">
        Matches that were played within a specific date range {q4.length}.
      </h3>

      <div className="container">
  <div className="row justify-content-center">
    <table className="table table-striped table-bordered">
      <thead>
        <tr>
        <th>Match Id</th>
            <th>Match date</th>
            <th>Venue </th>
            <th>Team 1</th>
            <th>Team 2</th>
        </tr>
      </thead>
      <tbody>
        {q4.map((p) => (
             <tr  key={p.matchId}>
              <td>{p.matchId}</td>
              <td>{p.matchDate}</td>
              <td>{p.venue}</td>
              <td>{p.team1Name}</td>
              <td>{p.team2Name}</td>
           </tr>
        ))}
      </tbody>
    </table>
  </div>
</div>


    </div>
  );
};

export default Range;
