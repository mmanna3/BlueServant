import { CamaDTO } from 'interfaces/habitacion';
import { IHabitacionParaTablaReservas } from 'interfaces/reserva';
import React, { ReactElement, useEffect, useState } from 'react';
import { useSelector } from 'react-redux';
import { tablaDeReservasSelector } from 'store/app/tablaDeReservas/slice';
import { obtenerDia } from 'utils/Fecha';
import Celda from '../Celda/Celda';

interface IParams {
  habitacionesConCamasUnificadas: IHabitacionParaTablaReservas[];
  mostrarDetalleDeReserva: (id: Nullable<number>) => void;
  mostrarDetalleDeHabitacion: (id: Nullable<number>) => void;
}

const Cuerpo = ({
  habitacionesConCamasUnificadas,
  mostrarDetalleDeReserva,
  mostrarDetalleDeHabitacion,
}: IParams): ReactElement => {
  const tablaDeReservas = useSelector(tablaDeReservasSelector);
  const [filas, actualizarFilas] = useState([]);

  useEffect((): void => {
    const renderizarCeldasDeLaFila = (cama: CamaDTO): ReactElement => {
      return (
        <>
          <td>
            {cama.nombre} - {cama.tipo}
          </td>
          {tablaDeReservas.dias.map(
            (dia): ReactElement => (
              <Celda
                key={`${obtenerDia(dia)}-${cama.id}`}
                dia={obtenerDia(dia)}
                camaId={cama.id}
                esHoy={false}
                onClick={mostrarDetalleDeReserva}
              />
            )
          )}
        </>
      );
    };

    let _filas: any = [];

    habitacionesConCamasUnificadas.forEach((habitacion): void => {
      _filas.push(
        <>
          <tr>
            <td rowSpan={habitacion.camas.length}>{habitacion.nombre}</td>
            {renderizarCeldasDeLaFila(habitacion.camas[0])}
          </tr>
          {habitacion.camas.slice(1).map(
            (cama, i): ReactElement => (
              <tr key={i}>{renderizarCeldasDeLaFila(cama)}</tr>
            )
          )}
        </>
      );
    });

    actualizarFilas(_filas);
  }, [tablaDeReservas.camasIdsArray, tablaDeReservas.dias, habitacionesConCamasUnificadas, mostrarDetalleDeReserva]);

  return <tbody>{filas}</tbody>;
};

export default Cuerpo;