using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ApiProcolombiaPQR.API.Services
{
    public class EjecutorDAO
    {
        private SqlConnection _Conexion = null;
        private SqlCommand _Comando = null;
        private SqlTransaction _Transaccion = null;

        private string _CadenaConexion;

        public EjecutorDAO(string nombreCadena)
        {
            configurar(nombreCadena);
        }

        private void configurar(string nombreCadena)
        {
            try
            {
                //this._CadenaConexion = ConfigurationManager.ConnectionString[nombreCadena].ConnectionString;
            }
            catch (DataException ex)
            {
                //throw new EjecutorDAOException("Error al cargar la configuración del acceso a datos.", ex);
            }

        }

        public void Desconectar()
        {
            if (this._Conexion.State.Equals(ConnectionState.Open))
            {
                this._Conexion.Close();
            }
        }

        public void Conectar()
        {
            if ((this._Conexion != null))
            {
                if (this._Conexion.State.Equals(ConnectionState.Closed))
                {
                    //throw new EjecutorDAOException("La conexión ya se encuentra abierta.");
                }
            }

            try
            {
                if (this._Conexion == null)
                {
                    this._Conexion = new SqlConnection();
                    this._Conexion.ConnectionString = _CadenaConexion;
                }
                this._Conexion.Open();
            }
            catch (DataException ex)
            {
                //throw new EjecutorDAOException("Error al conectarse." + ex.ToString());
            }
        }

        public void CrearComando(string sentenciaSQL)
        {
            this._Comando = new SqlCommand();
            this._Comando.Connection = this._Conexion;
            this._Comando.CommandType = CommandType.Text;
            this._Comando.CommandText = sentenciaSQL;
            if ((this._Transaccion != null))
            {
                this._Comando.Transaction = this._Transaccion;
            }
        }

        public void AsignarParametroCadena(string nombre, string valor)
        {
            AsignarParametro(nombre, "'", valor);
        }

        private void AsignarParametro(string nombre, string separador, string valor)
        {
            int indice = this._Comando.CommandText.IndexOf(nombre);
            string prefijo = this._Comando.CommandText.Substring(0, indice);
            string sufijo = this._Comando.CommandText.Substring(indice + nombre.Length);
            this._Comando.CommandText = prefijo + separador + valor + separador + sufijo;
        }

        public SqlDataReader EjecutarConsulta()
        {
            return this._Comando.ExecuteReader();
        }

    }

}
