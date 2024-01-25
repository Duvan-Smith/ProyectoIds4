import React, { useState } from 'react'
import { signoutRedirect, signoutRedirectCallback } from '../services/userService'
import { useSelector } from 'react-redux'
import { prettifyJson } from '../utils/jsonUtils'
import Axios from 'axios'
function Home() {
  const user = useSelector(state => state.auth.user)
  function signOut() {
    signoutRedirect()
  }

  async function obtenerDatosProtegidos() {
    // const usuario = await userManager.getUser();

    // if (usuario && !usuario.expired) {
    //   const tokenAcceso = usuario.access_token;

    //   try {
    //     const respuesta = await axios.get('https://localhost:7245/weatherforecast', {
    //       headers: {
    //         Authorization: `Bearer ${tokenAcceso}`,
    //       },
    //     });

    //     console.log('Datos protegidos:', respuesta.data);
    //   } catch (error) {
    //     console.error('Error al obtener datos protegidos:', error);
    //   }
    // }
    try {
      const respuesta = await Axios.get('https://localhost:7245/weatherforecast');

      console.log('Datos protegidos:', respuesta.data);
    } catch (error) {
      console.error('Error al obtener datos protegidos:', error);
    }
  }

  async function obtenerDatosProtegidosBlobStorage() {
    // const usuario = await userManager.getUser();

    // if (usuario && !usuario.expired) {
    //   const tokenAcceso = usuario.access_token;

    //   try {
    //     const respuesta = await axios.get('https://localhost:5012/BlobStorage/Archivo/GetListArchivo', {
    //       headers: {
    //         Authorization: `Bearer ${tokenAcceso}`,
    //       },
    //     });

    //     console.log('Datos protegidos:', respuesta.data);
    //   } catch (error) {
    //     console.error('Error al obtener datos protegidos:', error);
    //   }
    // }
    try {
      const respuesta = await Axios.get('https://localhost:5012/BlobStorage/Archivo/GetListArchivo');

      console.log('Datos protegidos BlobStorage:', respuesta.data);
    } catch (error) {
      console.error('Error al obtener datos protegidos BlobStorage:', error);
    }
  }

  return (
    <div>
      <p>Hello, <b>{user.profile.given_name}</b>.</p>
      <p>You have been signed in successfully!</p>
      <button className="button button-clear" onClick={() => obtenerDatosProtegidos()}>Probar Api</button>
      <button className="button button-clear" onClick={() => obtenerDatosProtegidosBlobStorage()}>Probar Api BlobStorage</button>
      <pre>
        <code>{prettifyJson(user)}</code>
      </pre>
      <button className="button button-clear" onClick={() => signOut()}>Sign Out</button>
    </div>
  )
}

export default Home