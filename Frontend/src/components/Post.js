import React, { useState } from "react";
import {AddPlayer} from "../Services/APIService";

const Post = (props) => {
  // const log = useContext(LogContext)

  const [player, setPlayer] = useState({
    PlayerName: "",
    TeamId: "",
    Role: "",
    Age: "",
    MatchesPlayed: "",
  });
  const createPlayer = async () => {
    // console.log(player);
    await AddPlayer(player);
  };
  const validate = (e) => {
    e.preventDefault();
    if (
      e.target.PlayerName.value === "" ||
      e.target.TeamId.value === "" ||
      e.target.Role.value === "" ||
      e.target.Age.value === "" ||
      e.target.MatchesPlayed.value === ""
    ) {
      alert("please fill all valid data");
      return false;
    } else {
      createPlayer();
    }
  };

  const onChange = (e) => {
    setPlayer({ ...player, [e.target.id]: e.target.value });
  };

  return (
    <>
      <form className="form-group container" onSubmit={validate}>
        <div className="form-group">
          <label htmlFor="player_name">Player Name : </label>
          <input
            type="text"
            className="form-control"
            id="PlayerName"
            value={player.PlayerName}
            onChange={onChange}
            style={{ borderRadius: "10px" }}
          />
        </div>

        <div className="form-group">
          <label htmlFor="team_id">team_id :</label>
          <input
            type="number"
            className="form-control"
            id="TeamId"
            value={player.TeamId}
            onChange={onChange}
            style={{ borderRadius: "10px" }}
          />
        </div>

        <div className="form-group">
          <label htmlFor="role">Role : </label>
          <input
            type="text"
            className="form-control"
            id="Role"
            value={player.Role}
            onChange={onChange}
            style={{ borderRadius: "10px" }}
          />
        </div>

        <div className="form-group">
          <label htmlFor="age">age</label>
          <input
            type="number"
            className="form-control"
            id="Age"
            value={player.Age}
            onChange={onChange}
            style={{ borderRadius: "10px" }}
          />
        </div>

        <div className="form-group">
          <label htmlFor="matches_played">matches_played</label>
          <input
            type="number"
            className="form-control"
            id="MatchesPlayed"
            value={player.MatchesPlayed}
            onChange={onChange}
            style={{ borderRadius: "10px" }}
          />
        </div>

        <button
          type="submit"
          className="btn btn-primary m-2 p-2"
          style={{ borderRadius: "10px" }}
        >
          Add Player
        </button>
      </form>
    </>
  );
};

export default Post;
