import React, { useEffect, useCallback } from 'react';
import { Modal, Body, Header, FooterVolver } from 'components/Modal';
import Display, { SiNo, DisplayLista, DisplayTextarea } from 'components/display/Display';
import api from 'store/api/api';
import { useDispatch, useSelector } from 'react-redux';
import { EstadosApiRequestEnum as ESTADO } from 'store/api/utils/estadosApiRequestEnum';

const Detalle = ({ onHide, id }) => {
  const dispatch = useDispatch();
  const { datos, estado } = useSelector(api.habitaciones.obtenerPorId.selector);

  const fetchData = useCallback(() => {
    if (id !== null) dispatch(api.habitaciones.obtenerPorId.invocar(id));
  }, [dispatch, id]);

  useEffect(() => fetchData(), [fetchData]);

  if (estado === ESTADO.exitoso) {
    var esPrivada = {
      true: 'Privada',
      false: 'Compartida',
    };

    function calcularMaximoDeCamas() {
      var maximo = datos.camasMatrimoniales.length;
      if (datos.camasIndividuales.length > datos.camasMatrimoniales.length) maximo = datos.camasIndividuales.length;
      if (datos.camasCuchetas.length > datos.camasIndividuales.length) maximo = datos.camasIndividuales.length;

      return maximo;
    }

    function ocultar() {
      onHide();
      dispatch(api.habitaciones.obtenerPorId.reiniciar());
    }

    const rowsDelTextAreaDeCamas = calcularMaximoDeCamas() + 1;

    return (
      <Modal isVisible={id !== null && estado === ESTADO.exitoso} onHide={ocultar}>
        <Header title="Detalle de habitación" onHide={ocultar} />
        <Body>
          <div className="columns">
            <div className="column">
              <Display label="Nombre" valor={datos.nombre} />
            </div>
            <div className="column">
              <Display label="Tipo" valor={esPrivada[datos.esPrivada]} />
            </div>
            <div className="column">
              <SiNo label="Tiene baño" valor={datos.tieneBanio} />
            </div>
          </div>
          <div className="columns">
            <div className="column">
              <DisplayLista
                label={`Camas Indiv. (${datos.camasIndividuales.length})`}
                lista={datos.camasIndividuales}
                rows={rowsDelTextAreaDeCamas}
                prop="nombre"
              />
            </div>
            <div className="column">
              <DisplayLista
                label={`Camas Matrim. (${datos.camasMatrimoniales.length})`}
                lista={datos.camasMatrimoniales}
                rows={rowsDelTextAreaDeCamas}
                prop="nombre"
              />
            </div>
            <div className="column">
              <DisplayLista
                label={`Camas Cuchetas (${datos.camasCuchetas.length})`}
                lista={datos.camasCuchetas}
                rows={rowsDelTextAreaDeCamas}
                prop="nombre"
              />
            </div>
          </div>
          <div className="columns">
            <div className="column">
              <DisplayTextarea label="Información adicional" valor={datos.informacionAdicional} />
            </div>
          </div>
        </Body>
        <FooterVolver onClick={ocultar} />
      </Modal>
    );
  }
  return <></>;
};

export default Detalle;
