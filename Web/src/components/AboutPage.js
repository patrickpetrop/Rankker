import React from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { bindActionCreators } from "redux";
import * as userActions from "../redux/actions/userActions";

class AboutPage extends React.Component {
  componentDidMount() {
    const { actions } = this.props;
    actions
      .getToken()
      .then(() => {})
      .catch(error => {
        console.log(error);
      });
  }

  render() {
    return (
      <div>
        {this.props.user && localStorage.setItem("accessToken", this.props.user.accessToken)}
        <h2>About Page</h2>
        <p>{this.props.user.userName}</p>
        <p>{this.props.user.accessToken}</p>
      </div>
    );
  }
}

AboutPage.propTypes = {
  actions: PropTypes.object.isRequired
};

function mapStateToProps(state) {
  return {
    user: state.user
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: {
      getToken: bindActionCreators(userActions.getToken, dispatch)
    }
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(AboutPage);
