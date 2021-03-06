import { createSlice as createSliceRTK, Dispatch, Slice } from '@reduxjs/toolkit';
import axios, { AxiosResponse } from 'axios';
import { EstadosApiRequestEnum as ESTADO } from './estadosApiRequestEnum';

export const initialState = {
  estado: ESTADO.inactivo,
  requestData: '',
  responseData: '',
  errores: undefined,
};

export const createSlice = (nombre: string): Slice =>
  createSliceRTK({
    name: nombre,
    initialState,
    reducers: {
      postInit: (state, { payload }): void => {
        state.estado = ESTADO.cargando;
        state.requestData = payload;
      },
      postSuccess: (state): void => {
        state.estado = ESTADO.exitoso;
        state.errores = undefined;
      },
      postFailure: (state, { payload }): void => {
        state.estado = ESTADO.huboError;
        state.errores = payload;
      },
      reset: (state): void => {
        state.estado = ESTADO.inactivo;
        state.responseData = '';
        state.requestData = '';
        state.errores = undefined;
      },
    },
  });

export function postFunc<TResultado, TPostBody>(
  endpoint: string,
  actions: any,
  data: TPostBody,
  onSuccess?: (responseData: any) => void
): (dispatch: Dispatch) => Promise<AxiosResponse<TResultado>> {
  const { postInit, postSuccess, postFailure } = actions;

  const funcionAsincronica = async (dispatch: Dispatch): Promise<any> => {
    dispatch(postInit());

    axios
      .post<TResultado>('/api' + endpoint, data)
      .then((res): void => {
        dispatch(postSuccess(res.data));
        typeof onSuccess === 'function' && onSuccess(res.data);
      })
      .catch((error): void => {
        dispatch(postFailure(error.response.data?.errors || error.response));
      });
  };

  return funcionAsincronica;
}

export function limpiarErrores(actions: any): (dispatch: Dispatch) => void {
  const { reset } = actions;

  return async (dispatch: Dispatch): Promise<any> => {
    dispatch(reset());
  };
}
