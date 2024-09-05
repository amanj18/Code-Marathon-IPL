import axios from "axios";

async function GetDataQ3() {
    const url = `http://localhost:5262/api/ipl/top-players`;
    let data = null;
    try {
        let response = await axios.get(url);
        if ( response.data !== null) {
            data = await response.data
        }
    }
    catch (error) {
        return JSON.stringify(error)
    }
    return data;

}

export {GetDataQ3};
