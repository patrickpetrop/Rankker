import * as types from "./actionTypes";
import {UserApi} from "../../api/rankker";

export function getTokenSuccess(user) {
  return { type: types.GET_TOKEN_SUCCESS, user };
}

export function getToken() {
  //dispatch is injected automaticallt from thunk, so we dont have to pass it
  return function(dispatch) {
    return UserApi
      .getToken()
      .then(user => {
        dispatch(getTokenSuccess(user));
      })
      .catch(error => {
        throw error;
      });
  };
}