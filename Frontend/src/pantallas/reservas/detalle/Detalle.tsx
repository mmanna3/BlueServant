import { Boton } from 'components/botones/botones';
import DatoConIcono from 'components/DatoConIcono/DatoConIcono';
import Modal, { TituloModal, CuerpoModal } from 'components/Modal/Modal';
import React, { ReactElement, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import api from 'store/api/api';
import { ReservaEstadoEnum } from 'store/api/DTOs';
import { convertirADate, nombreDelDiaDeLaSemana, nombreDelMes, restarFechas } from 'utils/Fecha';
import Cancelar from './Cancelar/Cancelar';
import Estilos from './Detalle.module.scss';
import HacerCheckIn from './HacerCheckIn/HacerCheckIn';
import HacerCheckOut from './HacerCheckOut/HacerCheckOut';
import MostrarHabitacionesYCamas from './MostrarHabitacionesYCamas/MostrarHabitacionesYCamas';

interface IProps {
  enCheckInExitoso: () => void;
  enCheckOutExitoso: () => void;
  enCancelacionExitosa: () => void;
}

const Detalle = ({ enCheckInExitoso, enCheckOutExitoso, enCancelacionExitosa }: IProps): ReactElement => {
  const [modalHacerCheckInEsVisible, cambiarVisibilidadDeModalHacerCheckIn] = useState(false);
  const [modalHacerCheckOutEsVisible, cambiarVisibilidadDeModalHacerCheckOut] = useState(false);
  const [modalCancelarEsVisible, cambiarVisibilidadDeModalCancelar] = useState(false);
  const [modalPrincipalEsVisible, cambiarVisibilidadDeModalPrincipal] = useState(true);
  const dispatch = useDispatch();
  const { datos } = useSelector(api.reservas.obtenerPorId.selector);

  function reiniciarDatos(): void {
    dispatch(api.reservas.obtenerPorId.reiniciar());
  }

  const fechaParaMostrar = (fecha: string): string => {
    var fechaDate = convertirADate(fecha);
    return `${nombreDelDiaDeLaSemana(fechaDate)} ${fechaDate.getDate()} ${nombreDelMes(fechaDate)}`;
  };

  const calcularCantidadDeNoches = (fechaCheckout: string, fechaCheckin: string): number => {
    var checkoutDate = convertirADate(fechaCheckout);
    var checkinDate = convertirADate(fechaCheckin);
    return restarFechas(checkoutDate, checkinDate);
  };

  const textoNoches = (fechaCheckout: string, fechaCheckin: string): string => {
    var cantidadDeNoches = calcularCantidadDeNoches(fechaCheckout, fechaCheckin);
    return cantidadDeNoches === 1 ? '1 noche' : `${cantidadDeNoches} noches`;
  };

  const textoPasajeros = (cantidad: number): string => (cantidad === 1 ? '1 pasajero' : `${cantidad} pasajeros`);
  interface IEstilo {
    estilo: string;
    descripcion: string;
  }

  const estilosEstado = new Map<ReservaEstadoEnum, IEstilo>([
    [ReservaEstadoEnum.Cancelada, { estilo: Estilos.estadoCancelada, descripcion: 'Cancelada' }],
    [ReservaEstadoEnum.CheckinPendiente, { estilo: Estilos.estadoCheckinPendiente, descripcion: 'Check-In Pendiente' }],
    [ReservaEstadoEnum.InHouse, { estilo: Estilos.estadoInHouse, descripcion: 'In-House' }],
    [ReservaEstadoEnum.HizoCheckout, { estilo: Estilos.estadoHizoCheckout, descripcion: 'Hizo Checkout' }],
  ]);

  return datos !== null ? (
    <>
      <HacerCheckIn
        datos={datos}
        esVisible={modalHacerCheckInEsVisible}
        alOcultar={(): void => {
          cambiarVisibilidadDeModalHacerCheckIn(false);
          cambiarVisibilidadDeModalPrincipal(true);
        }}
        enCheckInExitoso={(): void => {
          cambiarVisibilidadDeModalHacerCheckIn(false);
          cambiarVisibilidadDeModalPrincipal(true);
          reiniciarDatos();
          enCheckInExitoso();
        }}
      />
      <HacerCheckOut
        datos={datos}
        esVisible={modalHacerCheckOutEsVisible}
        alOcultar={(): void => {
          cambiarVisibilidadDeModalHacerCheckOut(false);
          cambiarVisibilidadDeModalPrincipal(true);
        }}
        enCheckOutExitoso={(): void => {
          cambiarVisibilidadDeModalHacerCheckOut(false);
          cambiarVisibilidadDeModalPrincipal(true);
          reiniciarDatos();
          enCheckOutExitoso();
        }}
      />
      <Cancelar
        datos={datos}
        esVisible={modalCancelarEsVisible}
        alOcultar={(): void => {
          cambiarVisibilidadDeModalCancelar(false);
          cambiarVisibilidadDeModalPrincipal(true);
        }}
        enCancelacionExitosa={(): void => {
          cambiarVisibilidadDeModalCancelar(false);
          cambiarVisibilidadDeModalPrincipal(true);
          reiniciarDatos();
          enCancelacionExitosa();
        }}
      />
      <Modal esVisible={modalPrincipalEsVisible} alOcultar={reiniciarDatos}>
        <TituloModal>
          {datos.pasajeroTitular.nombreCompleto}
          <span className={estilosEstado.get(datos.estado)?.estilo}>{estilosEstado.get(datos.estado)?.descripcion}</span>
        </TituloModal>
        <p className={Estilos.fechas}>
          {fechaParaMostrar(datos.diaDeCheckin)} → {fechaParaMostrar(datos.diaDeCheckout)}
        </p>
        <CuerpoModal>
          <DatoConIcono icono="calendar" texto={textoNoches(datos.diaDeCheckout, datos.diaDeCheckin)} />
          <DatoConIcono icono="user-friends" texto={textoPasajeros(datos.cantidadDePasajeros)} />
          <DatoConIcono icono="clock" texto={`Llega a las ${datos.horaEstimadaDeLlegada} hs.`} />

          <MostrarHabitacionesYCamas
            habitacionesPrivadas={datos.habitacionesPrivadas}
            camasDeHabitacionesCompartidas={datos.camas}
          />
          <div className={Estilos.botones}>
            {datos.estado === ReservaEstadoEnum.CheckinPendiente && (
              <>
                <div className="column">
                  <Boton
                    icono="times"
                    texto="Cancelar reserva"
                    className={Estilos.ocuparTodoElAncho}
                    onClick={(): void => {
                      cambiarVisibilidadDeModalPrincipal(false);
                      cambiarVisibilidadDeModalCancelar(true);
                    }}
                  />
                </div>
                <div className="column">
                  <Boton
                    icono="walking"
                    texto="Hacer Check-In"
                    className={`is-primary ${Estilos.ocuparTodoElAncho}`}
                    onClick={(): void => {
                      cambiarVisibilidadDeModalPrincipal(false);
                      cambiarVisibilidadDeModalHacerCheckIn(true);
                    }}
                  />
                </div>
              </>
            )}

            {datos.estado === ReservaEstadoEnum.InHouse && (
              <div className="column">
                <Boton
                  icono="walking"
                  texto="Hacer Check-out"
                  className={`is-primary ${Estilos.ocuparTodoElAncho}`}
                  onClick={(): void => {
                    cambiarVisibilidadDeModalPrincipal(false);
                    cambiarVisibilidadDeModalHacerCheckOut(true);
                  }}
                />
              </div>
            )}
          </div>
        </CuerpoModal>
      </Modal>
    </>
  ) : (
    <></>
  );
};

export default Detalle;
