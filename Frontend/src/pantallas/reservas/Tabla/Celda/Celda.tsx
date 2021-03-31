import * as React from 'react';
import { useState, useEffect, ReactElement } from 'react';
import { tablaDeReservasSelector, seleccionarTodasLasCeldasDeLaReserva } from 'store/app/tablaDeReservas/slice';
import { useSelector, useDispatch } from 'react-redux';
import estilos from './Celda.module.scss';
import { ICeldaPertenecienteAReservaInfo } from './interfaces';

export interface IParams {
  dia: number;
  camaId: number;
  esHoy: boolean;
  onClick: (id: Nullable<number>) => any;
}

const Celda = ({ dia, camaId, onClick }: IParams): ReactElement => {
  const dispatch = useDispatch();
  const { tabla } = useSelector(tablaDeReservasSelector);
  const [contenido, actualizarContenido] = useState<ICeldaPertenecienteAReservaInfo>({} as ICeldaPertenecienteAReservaInfo);
  const [claseCssColor, actualizarClaseCssColor] = useState<string | undefined>('');

  // const claseCssEsHoy = useState<string | undefined>(esHoy ? estilos.esHoy : '');

  const colores = new Map<number, string>([
    [0, estilos.colorCero],
    [1, estilos.colorUno],
    [2, estilos.colorDos],
    [3, estilos.colorTres],
    [4, estilos.colorCuatro],
    [5, estilos.colorCinco],
    [6, estilos.colorSeis],
    [7, estilos.colorSiete],
    [8, estilos.colorOcho],
    [9, estilos.colorNueve],
  ]);

  const onMouseOver = (): void => {
    dispatch(seleccionarTodasLasCeldasDeLaReserva(contenido.id));
  };

  useEffect((): void => {
    var contenido = tabla[`${dia}`][`${camaId}`];
    actualizarContenido(contenido);

    if (contenido.id !== null) {
      //Horrible if, hay que sacarlo
      var codigoColorSegunTerminacionDelId = contenido.id % 10;
      actualizarClaseCssColor(colores.get(codigoColorSegunTerminacionDelId));
    }
  }, [tabla, dia, camaId, colores]);

  return (
    <>
      <td
        className={`${claseCssColor} ${contenido.estilo}`}
        data-reserva-id={contenido.id}
        data-dia={dia}
        data-cama-id={camaId}
        onMouseOver={onMouseOver}
        onClick={(): void => onClick(contenido.id)}
      >
        <div>{/* {contenido.aNombreDe} */}</div>
      </td>
    </>
  );
};

export default Celda;
