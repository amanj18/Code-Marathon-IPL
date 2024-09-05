import axios from "axios";

const AddPlayer = async (player) => {
  const URL = `http://localhost:5262/api/ipl/add`;
  console.log(player);
  await axios
    .post(URL, player)
    .then(() => {
      alert("Player added successfully");
    })
    .catch((err) => {
      alert("Error 400: Bad Request. Please check your input data.");
    });
};

export default AddPlayer;
