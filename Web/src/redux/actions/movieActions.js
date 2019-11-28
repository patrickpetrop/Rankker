import * as types from "./actionTypes";
import {MovieApi} from "../../api/rankker";

export function loadMoviesSuccess(movies) {
  return { type: types.LOAD_MOVIES_SUCCESS, movies };
}

export function loadMovies() {
  //dispatch is injected automaticallt from thunk, so we dont have to pass it
  return function(dispatch) {
    return MovieApi
      .getAllMovies()
      .then(movies => {
        dispatch(loadMoviesSuccess(movies));
      })
      .catch(error => {
        throw error;
      });
  };
}