import AnimacionCargando from 'components/Tabla/AnimacionCargando/AnimacionCargando';
import ErrorGenerico from 'components/Tabla/ErrorGenerico/ErrorGenerico';
import React, { ReactElement, useCallback, useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import api from 'store/api/api';
import { EstadosApiRequestEnum as ESTADO } from 'store/api/utils/estadosApiRequestEnum';
import { useCounterKey } from 'utils/hooks/useCounterKey';
import Cabecera from './Cabecera/Cabecera';
import Crear from './crear/Crear';
import DetalleReserva from './detalle/DetalleReserva';
import Estilos from './PantallaReservas.module.scss';
import Tabla from './Tabla/Tabla';

const ReservasPage = (): ReactElement => {
  const dispatch = useDispatch();
  const habitaciones = useSelector(api.habitaciones.listar.selector);
  const { estado: estadoDetalle } = useSelector(api.reservas.obtenerPorId.selector);
  const { datos, estado } = useSelector(api.reservas.listarVigentes.selector);
  const [IsModalVisible, setModalVisibility] = useState(false);
  const [cabeceraKey, reiniciarCabecera] = useCounterKey();

  const fetchData = useCallback((): void => {
    dispatch(api.habitaciones.listar.invocar());
  }, [dispatch]);

  useEffect((): void => fetchData(), [fetchData]);

  function onFechaChange(primeraNoche: string, dias: number): void {
    dispatch(api.reservas.listarVigentes.invocar({ primeraNoche, dias }));
  }

  function hideModal(): void {
    setModalVisibility(false);
  }

  function showModal(): void {
    setModalVisibility(true);
  }

  function reiniciarTabla(): void {
    reiniciarCabecera();
    fetchData();
  }

  function cerrarModalDeCreacionYReiniciarTabla(): void {
    hideModal();
    reiniciarTabla();
  }

  return (
    <div className={Estilos.contenedorDeTabla}>
      {IsModalVisible && (
        <Crear
          isVisible={IsModalVisible}
          onHide={hideModal}
          onSuccessfulSubmit={cerrarModalDeCreacionYReiniciarTabla}
        ></Crear>
      )}

      <Cabecera key={cabeceraKey} showModal={showModal} onFechaChange={onFechaChange} />
      <DetalleReserva ejecutarAlTerminar={reiniciarTabla} />
      <div>
        {estado === ESTADO.huboError || estadoDetalle === ESTADO.huboError ? (
          <table className="table is-fullwidth">
            <ErrorGenerico />
          </table>
        ) : estado === ESTADO.cargando || estadoDetalle === ESTADO.cargando ? (
          <table className="table is-fullwidth">
            <AnimacionCargando />
          </table>
        ) : estado === ESTADO.exitoso && datos ? (
          <Tabla datos={datos} habitaciones={habitaciones.datos} />
        ) : (
          <></>
        )}
      </div>
    </div>
  );
};

export default ReservasPage;
