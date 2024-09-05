import axios from "axios";
const res = `http://localhost:5262/api/ipl`;

const AddPlayer = async (player) => {
  const URL = `${res}/add`;
  await axios
    .post(URL, player)
    .then(() => {
      alert("Player added successfully");
    })
    .catch((err) => {
      alert("Error 400: Bad Request. Please check your input data.");
    });
};

async function MatchDetails() {
  let data = null;
  try {
    let response = await axios.get(`${res}/details`);
    if (response.data !== null) {
      data = await response.data;
      // console.log("Data from api" + JSON.stringify(data));
    }
  } catch (error) {
    return JSON.stringify(error);
  }
  return data;
}

async function TopPlayers() {
    let data = null;
    try {
        let response = await axios.get(`${res}/top-players`);
        if ( response.data !== null) {
            data = await response.data
        }
    }
    catch (error) {
        return JSON.stringify(error)
    }
    return data;
}

async function DateRange(start,end) {
    let data = null;
    try {
      let response = await axios.get(`${res}/by-date-range?startDate=${start}&endDate=${end}`);
      if (response.data !== null) {
        data = await response.data;
      }
    } catch (error) {
      return JSON.stringify(error);
    }
    return data;
  }
  

export { MatchDetails ,  TopPlayers, DateRange, AddPlayer };
