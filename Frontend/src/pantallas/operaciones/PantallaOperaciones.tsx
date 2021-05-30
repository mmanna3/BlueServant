import Tab from 'components/Tabs/Tab';
import ContenedorDeTabs from 'components/Tabs/ContenedorDeTabs';
import React, { ReactElement } from 'react';
import TodasLasReservas from './Tabs/TodasLasReservas';
import { ReservaEstadoEnum } from 'store/api/DTOs';
import { convertirAString, hoy } from 'utils/Fecha';
import { useParams } from 'react-router-dom';
// import { useParams } from 'react-router-dom';

const PantallaOperaciones = (): ReactElement => {
  interface IUrlParams {
    id: string;
  }

  let { id } = useParams<IUrlParams>();

  return (
    <div className="container">
      <h1 className="title is-2">Operaciones</h1>
      <ContenedorDeTabs>
        <Tab
          id={1}
          texto="Todas las reservas"
          icono="calendar"
          seleccionadaPorDefecto={id === '1'}
          contenido={<TodasLasReservas verFiltros={true} />}
        />
        <Tab
          id={2}
          texto="Check-Ins de hoy"
          icono="walking"
          seleccionadaPorDefecto={id === '2'}
          contenido={
            <TodasLasReservas
              verFiltros={false}
              estadoInicial={ReservaEstadoEnum.CheckinPendiente}
              checkInDesde={convertirAString(hoy())}
              checkInHasta={convertirAString(hoy())}
            />
          }
        />
      </ContenedorDeTabs>
    </div>
  );
};

export default PantallaOperaciones;
