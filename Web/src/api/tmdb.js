import * as keys from "./apiKeys";
import axios from "axios";

export const BASE_URL = "https://api.themoviedb.org/3/";

export function getBasicMovie() {
  return axios
    .get(BASE_URL + "movie/550?api_key=" + keys.THE_MOVIE_DB_API_KEY)
    .then(result => {
      console.log(result.data);
      return result.data;
    });
}
