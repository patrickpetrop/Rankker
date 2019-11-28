import React from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { bindActionCreators } from "redux";
import * as movieActions from "../../redux/actions/movieActions";
import MovieListItem from "./MovieListItem";

class MoviePage extends React.Component {
  componentDidMount() {
    const { movies, actions } = this.props;
    if (movies.length === 0) {
      actions.loadMovies().catch(error => {
        alert("Loading authors failed " + error);
      });
    }
  }

  render() {
    return (
      <div>
        <h2>Hello</h2>
        <h2>Logged in user is {this.props.user.userName}</h2>
        <p>Length of movie array is {this.props.movies.length}</p>
        <table>
          <tbody>
            {this.props.movies.map((movie, i) => {
              return <MovieListItem key={i} movie={movie} />;
            })}
          </tbody>
        </table>
      </div>
    );
  }
}

MoviePage.propTypes = {
  movies: PropTypes.array.isRequired,
  user: PropTypes.object.isRequired,
  actions: PropTypes.object.isRequired
};

function mapStateToProps(state) {
  return {
    movies: state.movies,
    user: state.user
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: {
      loadMovies: bindActionCreators(movieActions.loadMovies, dispatch)
    }
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(MoviePage);
