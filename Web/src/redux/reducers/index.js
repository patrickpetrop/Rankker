import { combineReducers } from "redux";
//remaned since it is default export in file
import movies from "./movieReducer";
import user from './userReducer';

const rootReducer = combineReducers({
  movies,
  user
});

export default rootReducer;