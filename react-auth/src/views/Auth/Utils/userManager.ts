import { UserManager } from "oidc-client";
import { createUserManager } from "redux-oidc";

console.log(
  `${window.location.protocol}//${window.location.hostname}${
    window.location.port ? `:${window.location.port}` : ""
  }/login-oidc-callback`
);
const settings = {
  authority: "https://localhost:7132/",
  redirect_uri: `${window.location.protocol}//${window.location.hostname}${
    window.location.port ? `:${window.location.port}` : ""
  }/login-oidc-callback`,
  silent_redirect_uri: `${window.location.protocol}//${
    window.location.hostname
  }${window.location.port ? `:${window.location.port}` : ""}/silent_renew.html`,
  post_logout_redirect_uri: `${window.location.protocol}//${
    window.location.hostname
  }${
    window.location.port ? `:${window.location.port}` : ""
  }/signout-oidc-callback`,
  automaticSilentRenew: true,
  filterProtocolClaims: true,
  client_id: "oidcMVCApp",
  secret: "ProCodeGuide",
  response_type: "code",
  scope: "weatherApi.read",
  loadUserInfo: true,
  monitorSession: false,
  response_mode: "query",
  webAuthResponseType: "code",
};
const userManager: UserManager = createUserManager(settings);

export default userManager;
