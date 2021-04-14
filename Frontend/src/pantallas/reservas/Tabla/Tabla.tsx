import React, { useEffect, useState, ReactElement } from 'react';
import Celda from './Celda/Celda';
import Estilos from './Tabla.module.scss';
import { useDispatch, useSelector } from 'react-redux';
import { inicializarTabla, tablaDeReservasSelector, insertarReserva } from 'store/app/tablaDeReservas/slice';
import Encabezado from './Encabezado/Encabezado';
import { obtenerAnio, obtenerMes, obtenerDia } from 'utils/Fecha';
import DetalleHabitacion from 'pantallas/habitaciones/detalle/Modal';
import { ReservasDelPeriodoDTO, IHabitacionParaTablaReservas, ReservaResumenDTO } from 'interfaces/reserva';
import { CamaDTO, HabitacionDTO } from 'interfaces/habitacion';
import Detalle from 'pantallas/reservas/detalle/Modal';
import EncabezadoDias from './EncabezadoDias/EncabezadoDias';

interface IParams {
  datos: ReservasDelPeriodoDTO;
  habitaciones: HabitacionDTO[];
}

interface IDiaMes {
  dia: number;
  mes: number;
}

const TablaReservas = ({ datos, habitaciones }: IParams): ReactElement => {
  const dispatch = useDispatch();
  const [habitacionesConCamasUnificadas, setHabitacionesConCamasUnificadas] = useState<IHabitacionParaTablaReservas[]>([]);
  const [filas, actualizarFilas] = useState([]);
  const tablaDeReservas = useSelector(tablaDeReservasSelector);

  useEffect((): void => {
    var _dias: IDiaMes[] = [];

    function calcularDias(): void {
      var mesDesde = obtenerMes(datos.desde);
      var mesHasta = obtenerMes(datos.hasta);

      if (mesDesde === mesHasta) {
        for (let dia = obtenerDia(datos.desde); dia <= obtenerDia(datos.hasta); dia++) {
          _dias.push({ dia: dia, mes: mesDesde });
        }
      } else {
        var diasDelPrimerMes = new Date(obtenerAnio(datos.desde), obtenerMes(datos.desde), 0).getDate(); //dia 0 es el último día del mes anterior
        for (let dia = obtenerDia(datos.desde); dia <= diasDelPrimerMes; dia++) {
          _dias.push({ dia: dia, mes: mesDesde });
        }
        for (let dia = 1; dia <= obtenerDia(datos.hasta); dia++) {
          _dias.push({ dia: dia, mes: mesHasta });
        }
      }
    }

    calcularDias();

    var camasIdsArray: number[] = [];
    var habs: IHabitacionParaTablaReservas[] = [];
    for (let i = 0; i < habitaciones.length; i++) {
      var habitacion = habitaciones[i];
      var camasDeLaHabitacion = habitacion.camasIndividuales;
      camasDeLaHabitacion = camasDeLaHabitacion.concat(habitacion.camasMatrimoniales);
      camasDeLaHabitacion = camasDeLaHabitacion.concat(habitacion.camasCuchetas.map((cucheta): CamaDTO => cucheta.abajo));
      camasDeLaHabitacion = camasDeLaHabitacion.concat(habitacion.camasCuchetas.map((cucheta): CamaDTO => cucheta.arriba));
      habs.push({
        nombre: habitacion.nombre,
        id: habitacion.id,
        esPrivada: habitacion.esPrivada,
        camas: camasDeLaHabitacion,
      });
      camasIdsArray = camasIdsArray.concat(camasDeLaHabitacion.map((cama): number => cama.id));
    }
    setHabitacionesConCamasUnificadas(habs);

    dispatch(inicializarTabla(_dias, camasIdsArray));

    datos.reservas.forEach((reserva: ReservaResumenDTO): void => {
      dispatch(insertarReserva(reserva));
    });
  }, [datos.desde, datos.hasta, datos.reservas, dispatch, habitaciones]);

  const [idSeleccionadoParaDetalle, cambiarIdSeleccionadoParaDetalle] = useState<Nullable<number>>(null);
  const [idSeleccionadoParaDetalleHabitacion, cambiarIdSeleccionadoParaDetalleHabitacion] = useState<Nullable<number>>(null);

  function mostrarDetalleReserva(id: Nullable<number>): void {
    cambiarIdSeleccionadoParaDetalle(id);
  }

  function ocultarDetalleReserva(): void {
    cambiarIdSeleccionadoParaDetalle(null);
  }

  function mostrarDetalleDeHabitacion(id: number): void {
    cambiarIdSeleccionadoParaDetalleHabitacion(id);
  }

  function ocultarDetalleHabitacion(): void {
    cambiarIdSeleccionadoParaDetalleHabitacion(null);
  }

  useEffect((): void => {
    let _filas: any = [];
    var diaDeHoy = new Date().getDate(); // Claramente, cuando seleccionás un mes que no es el actual esto no anda

    tablaDeReservas.diaMesArray.forEach((diaMes): void => {
      if (diaMes.dia !== diaDeHoy)
        _filas.push(
          <tr key={diaMes.dia}>
            <th className={`has-text-weight-medium ${Estilos.fecha}`}>
              {diaMes.dia}/{diaMes.mes}
            </th>
            {tablaDeReservas.camasIdsArray.map(
              (id): ReactElement => (
                <Celda key={id} dia={diaMes.dia} camaId={id} esHoy={false} onClick={mostrarDetalleReserva} />
              )
            )}
          </tr>
        );
      else
        _filas.push(
          <tr key={diaMes.dia}>
            <th className={`has-text-weight-medium ${Estilos.hoy}`}>HOY</th>
            {tablaDeReservas.camasIdsArray.map(
              (id): ReactElement => (
                <Celda key={id} dia={diaMes.dia} camaId={id} esHoy={true} onClick={mostrarDetalleReserva} />
              )
            )}
          </tr>
        );
    });

    actualizarFilas(_filas);
  }, [tablaDeReservas.camasIdsArray, tablaDeReservas.diaMesArray]);

  return (
    <>
      <DetalleHabitacion id={idSeleccionadoParaDetalleHabitacion} onHide={ocultarDetalleHabitacion}></DetalleHabitacion>
      <Detalle id={idSeleccionadoParaDetalle} onHide={ocultarDetalleReserva}></Detalle>
      <div className={Estilos.contenedor}>
        <table className={`table is-hoverable is-bordered is-fullwidth`}>
          <EncabezadoDias fechaInicio={datos.desde} cantidadDeDias={15} />
          {/* <Encabezado habitaciones={habitacionesConCamasUnificadas} mostrarDetalle={mostrarDetalleDeHabitacion} /> */}
          {/* <tbody>{filas}</tbody> */}
        </table>
      </div>
    </>
  );
};

export default TablaReservas;
