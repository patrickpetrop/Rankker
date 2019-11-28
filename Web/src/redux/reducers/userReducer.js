import * as types from "../actions/actionTypes";
import initialState from "./initialState";

export default function userReducer(state = initialState.user, action) {
  switch (action.type) {
    case types.GET_TOKEN_SUCCESS:
      console.log(action);
      return action.user;
    default:
      return state;
  }
}
