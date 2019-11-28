import axios from "axios";

export const BASE_URL = "https://localhost:44390/";

export type MovieGenre = {
  source: String,
  sourceName: String,
  sourceId: Number
};

export type Movie = {
  imdbId: String,
  movieGenres: Array<MovieGenre>,
  name: String,
  overView: String,
  releaseDate: String,
  runTime: Number,
  tmdbBackdropPath: String,
  tmdbId: Number,
  tmdbPosterPath: String
};

const transformMovie = (movie: Object): Movie => ({
  ...movie
});

export const MovieApi = {
  getAllMovies(): Promise<Array<Movie>> {
    return axios.get(BASE_URL + "api/populate").then(result => {
      console.log(result.data);
      return result.data
        .sort(() => Math.random() - Math.random())
        .slice(0, 15)
        .map(transformMovie);
    });
  }
};

export type Token = {
  access_Token: String,
  userName: String
};
const transformToken = (token: Object): Token => ({
  ...token
});

export const UserApi = {
  getToken(): Promise<Token> {
    return axios
      .post(BASE_URL + "token?username=p1&password=Pwd12345.")
      .then(result => {
        return result.data;
      });
  }
};
