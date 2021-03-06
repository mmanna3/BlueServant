import { Boton } from 'components/botones/botones';
import { Icon } from 'components/Icon';
import Table from 'components/Tabla/Tabla';
import React, { ReactElement, useCallback, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import api from 'store/api/api';
import Crear from './crear/Modal';
import Detalle from './detalle/Modal';

const HabitacionesPage = (): ReactElement => {
  const dispatch = useDispatch();
  const [seMuestraModalDeCreacion, mostrarModalDeCreacion] = useState(false);
  const { datos, estado } = useSelector(api.habitaciones.listar.selector);
  const { estado: estadoDetalle } = useSelector(api.habitaciones.obtenerPorId.selector);

  const columnas = [
    {
      Header: '',
      accessor: 'id',
      mostrar: false,
    },
    {
      Header: 'Nombre',
      accessor: 'nombre',
    },
    {
      id: 'estaHabilitada',
      Header: 'Habilitada',
      accessor: (d: any): string => (d.estaHabilitada ? 'Sí' : 'No'),
    },
    {
      Header: 'Camas matrimoniales',
      accessor: 'camasMatrimoniales.length',
    },
    {
      Header: 'Camas cuchetas',
      accessor: 'camasCuchetas.length',
    },
    {
      Header: 'Camas individuales',
      accessor: 'camasIndividuales.length',
    },
    {
      Header: '',
      id: 'aciones',
      Cell: ({ cell }: any): ReactElement => (
        <Icon
          faCode="info-circle"
          size="lg"
          cssClass="primary-clickeable"
          onClick={(): void => {
            dispatch(api.habitaciones.obtenerPorId.invocar({ id: cell.row.values.id }));
          }}
        />
      ),
    },
  ];

  const fetchData = useCallback((): void => {
    dispatch(api.habitaciones.listar.invocar());
  }, [dispatch]);

  function cerrarModalDeCreacionYRefrescarTabla(): void {
    mostrarModalDeCreacion(false);
    fetchData();
  }

  return (
    <div className="container">
      <Crear
        isVisible={seMuestraModalDeCreacion}
        onHide={(): void => mostrarModalDeCreacion(false)}
        onSuccessfulSubmit={cerrarModalDeCreacionYRefrescarTabla}
      ></Crear>
      <Detalle enDeshabilitacionExitosa={fetchData} enHabilitacionExitosa={fetchData} />

      <h1 className="title is-2">Habitaciones</h1>
      <div className="botonera">
        <div className="is-pulled-right">
          <Boton onClick={(): void => mostrarModalDeCreacion(true)} texto="Cargar nueva" />
        </div>
      </div>
      <Table fetchData={fetchData} columnas={columnas} datos={datos} estado={estado || estadoDetalle} />
    </div>
  );
};

export default HabitacionesPage;
