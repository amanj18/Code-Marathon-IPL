import axios from "axios";

async function GetDataQ4(start,end) {
  const url = `http://localhost:5262/api/ipl/by-date-range?startDate=${start}&endDate=${end}`;
  let data = null;
  try {
    let response = await axios.get(url);
    if (response.data !== null) {
      data = await response.data;
    }
  } catch (error) {
    return JSON.stringify(error);
  }
  return data;
}

export { GetDataQ4 };
