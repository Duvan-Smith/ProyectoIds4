import React, { useState, useEffect } from 'react';
import { UserManager, WebStorageStateStore } from 'oidc-client';

const apiUrl = 'https://localhost:7245/weatherforecast';

const App = () => {
  const [user, setUser] = useState(null);
  const [data, setData] = useState(null);
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const setting = {
    authority: 'https://localhost:7132',
    client_id: 'oidcMVCAppR',
    secret: "ProCodeGuide",
    redirect_uri: 'https://localhost:3000/login-oidc-callback',
    post_logout_redirect_uri: 'https://localhost:3000',
    response_type: 'code',
    scope: 'openid profile email role weatherApi.read',
    loadUserInfo: true,
    monitorSession: false,
    response_mode: "query",
    webAuthResponseType: "code",
    extraQueryParams: {
      code_challenge_method: "S256",
      code_challenge: "O0LhWq_i9UiRVg0voufUT7J7ZzGpnSdJFSvSeIXu_aU",
    },
    userStore: new WebStorageStateStore({ store: window.localStorage }),
  };

  useEffect(() => {
    const loadUser = async () => {
      const userManager = new UserManager(setting);

      const user = await userManager.getUser();
      setUser(user);
    };

    loadUser();
  }, []);

  const userData = async () => {
    const userManager = new UserManager(setting);

    const user = await userManager.getUser();
    console.log(user);
    setUser(user);
  }

  const handleLogin = async () => {
    const userManager = new UserManager(setting);

    // const loginResult = await userManager.signinRedirect({
    //   data: { secret: password },
    // });

    try {
      // const loginResult = await userManager.signinRedirect();
      const loginResult = await userManager.signinRedirect();

      // Manejar diferentes situaciones basadas en el resultado del inicio de sesión
      switch (loginResult.status) {
        case 'success':
          // El inicio de sesión fue exitoso, puedes realizar acciones adicionales si es necesario
          console.log(loginResult.JSON);
          break;
        case 'redirect':
          // La aplicación se ha redirigido al servidor de autorización para el inicio de sesión
          console.log(loginResult.JSON);
          break;
        case 'error':
          // Hubo un error durante el inicio de sesión, puedes manejarlo adecuadamente
          console.error('Error durante el inicio de sesión:', loginResult.error);
          break;
        default:
          break;
      }
    } catch (error) {
      // Manejar errores generales
      console.error('Error general durante el inicio de sesión:', error);
    }
  };

  const handleLogout = async () => {
    const userManager = new UserManager(setting);

    // await userManager.signoutRedirect();
    await userManager.signoutRedirect();
  }

  const fetchData = async () => {
    const token = user?.access_token;

    if (token) {
      try {
        const response = await fetch(apiUrl, {
          method: 'GET',
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (response.ok) {
          const result = await response.json();
          setData(result);
        } else {
          console.error('Error al recuperar datos de la API');
        }
      } catch (error) {
        console.error('Error en la solicitud a la API', error);
      }
    }
  };

  return (
    <div>
      {user ? (
        <div>
          <p>Bienvenido, {user.profile.name}!</p>
          <button onClick={fetchData}>Obtener Datos de la API</button>
          <button onClick={handleLogout}>Cerrar Sesión</button>
        </div>
      ) : (
        <div>
          <p>No estás autenticado. Por favor, inicia sesión.</p>
          <label>
            Usuario:
            <input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </label>
          <label>
            Contraseña:
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </label>
          <button onClick={handleLogin}>Iniciar Sesión</button>
          <button onClick={handleLogout}>Cerrar Sesión</button>
          <button onClick={userData}>User data</button>
        </div>
      )}

      {data && (
        <div>
          <h2>Datos de la API:</h2>
          <pre>{JSON.stringify(data, null, 2)}</pre>
        </div>
      )}
    </div>
  );
};

export default App;