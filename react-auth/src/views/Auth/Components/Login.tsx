import { useState } from "react";
import userManager from "../Utils/userManager";

const Login = () => {
  var [loading, setLoading] = useState(false);
  const year = () => {
    const date = new Date();
    return date.getFullYear();
  };

  const login = async () => {
    try {
      setLoading(true);
      await userManager.signinRedirect();
      setLoading(false);
    } catch ($e) {
      setLoading(false);
    }
  };

  return (
    <div className="bg-login" style={{ height: "100vh" }}>
      <div
        style={{ height: "100vh" }}
        className="justify-content-end align-content-center"
      >
        <div className="col-md-6 J-login-content-bg">
          <div className="J-login-content-bg-after"></div>
          <div className="J-login-content-bg-before">
            <div className="J-login-logo">
              <img src="/logo-1.png" alt="" />
            </div>
          </div>
        </div>
        <div className="col-md-6 align-content-center J-login-content">
          <div className="J-login-content-card">
            <div className="mx-3 my-4 shadow shadow-lg J-card-login">
              <div className="text-center mx-4">
                <div className="my-3">
                  <div className="J-login-title">
                    Sistema Centralizado de <b>Seguridad Vial</b>
                  </div>
                </div>
                <button className="mb-5" onClick={login}>
                  <i className="fa fa-sign-in-alt" /> Iniciar Sesión
                  {loading ? (
                    <>
                      <div
                        className="J-spinner spinner-border text-light"
                        role="status"
                      >
                        <span className="visually-hidden">Loading...</span>
                      </div>
                    </>
                  ) : (
                    <></>
                  )}
                </button>
                <p className="mb-3 text-center text-secondary">
                  © S&P Solutions {year()}. Todos los derechos reservados.
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
