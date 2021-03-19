import { useDispatch, useSelector } from 'react-redux';
import { useCallback } from 'react';
import { fetchHabitacionesConLugaresLibres, habitacionesSelector } from './slice';
import { IUseListarHookRespuesta } from 'store/interfaces';

export function useListar(): IUseListarHookRespuesta {
  const dispatch = useDispatch();
  const { estado, datos } = useSelector(habitacionesSelector);

  const listar = useCallback(
    (desde: string, hasta: string): any => dispatch(fetchHabitacionesConLugaresLibres(desde, hasta)),
    [dispatch]
  );

  return {
    estado,
    datos,
    listar,
  };
}

export default useListar;
