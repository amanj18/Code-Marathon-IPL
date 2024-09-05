import "bootstrap/dist/css/bootstrap.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import Banner from "./components/Banner";
import Details from "./components/MatchDetails";
import TopPlays from "./components/TopPlayers";
import Navigation from "./components/Navigation";
import Post from "./components/Post";
import Range from "./components/DateRange";

function App() {
  return (
    <>
      <BrowserRouter>
        <Navigation />
        <Routes>
          <Route path="/" element={<Banner />} />
          <Route path="/Match-Details" element={<Details />} />
          <Route path="/top-players" element={<TopPlays />} />
          <Route path="/post" element={<Post />} />
          <Route path="/date-range" element={<Range />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
