import React from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { bindActionCreators } from "redux";

class HomePage extends React.Component {
  render() {
    return (
      <div>
        <p>Home Page</p>
        <h2>Logged in user is {this.props.user.userName}</h2>
      </div>
    );
  }
}
HomePage.propTypes = {
  user: PropTypes.object.isRequired
};

function mapStateToProps(state) {
  return {
    user: state.user
  };
}

export default connect(mapStateToProps)(HomePage);
