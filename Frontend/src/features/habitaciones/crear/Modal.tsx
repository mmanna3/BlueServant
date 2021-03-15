import React, { ReactElement } from 'react';
import { ModalForm, Body, Header, FooterAcceptCancel } from 'components/Modal';
import { Input } from 'components/Input';
import { Button } from 'components/botones/botones';
import Select from 'components/Select';
import ValidationSummary from 'components/ValidationSummary';
import Label from 'components/Label';
import SelectCama from './SelectCama';
import SiNo from 'components/SiNo';
import Textarea from 'components/Textarea';
import useStore from './useStore';

interface IProps {
  isVisible: boolean;
  onHide: () => void;
  onSuccessfulSubmit: () => void;
}

interface IRenglonCama {
  index: number;
  tipo: string;
  globalIndex: number;
  value?: object;
}

const Crear = ({ isVisible, onHide, onSuccessfulSubmit }: IProps): ReactElement => {
  const [resetOnChanged, resetForm] = React.useState(0);
  const [camas, setCamas] = React.useState<IRenglonCama[]>([{ index: 0, tipo: 'Individuales', globalIndex: 0, value: {} }]);

  const { loading, validationErrors, cleanErrors, agregarHabitacion } = useStore.habitaciones();

  function onSuccess(): void {
    onSuccessfulSubmit();
    resetForm(resetOnChanged + 1);
    setCamas([{ index: 0, tipo: 'Individuales', globalIndex: 0, value: {} }]);
  }

  const onSubmit = (data: any): void => {
    agregarHabitacion(data, onSuccess);
  };

  function hide(): void {
    onHide();
    cleanErrors();
    setCamas([{ index: 0, tipo: 'Individuales', globalIndex: 0, value: {} }]);
  }

  function getNextCamaIndex(array: any, tipo: string): number {
    var cama = array
      .slice()
      .reverse()
      .find((x: any): boolean => x.tipo === tipo);
    return cama ? cama.index + 1 : 0;
  }

  function getNextGlobalIndex(array: any): number {
    var camasReverse = array.slice().reverse();
    return camasReverse[0].globalIndex + 1;
  }

  function updateCamaIndexes(array: IRenglonCama[]): void {
    function updatePorTipo(array: IRenglonCama[], tipo: string): void {
      var arrayDelTipo = array.filter((x): boolean => x.tipo === tipo);

      for (let i = 0; i < arrayDelTipo.length; i++) if (arrayDelTipo[i].index !== i) arrayDelTipo[i].index = i;
    }

    updatePorTipo(array, 'Individuales');
    updatePorTipo(array, 'Matrimoniales');
    updatePorTipo(array, 'Cuchetas');
  }

  function addCama(): void {
    var nextIndex = getNextCamaIndex(camas, 'Individuales');
    setCamas((prevIndexes: IRenglonCama[]): IRenglonCama[] => [
      ...prevIndexes,
      { index: nextIndex, tipo: 'Individuales', globalIndex: getNextGlobalIndex(camas) },
    ]);
  }

  const removeCama = (globalIndex: number): (() => void) => (): void => {
    if (camas.length > 1) {
      var newArray = camas.filter((item): boolean => item.globalIndex !== globalIndex);
      updateCamaIndexes(newArray);
      setCamas(newArray);
    }
  };

  function setValue(globalIndex: number, value: object): void {
    var newArray = [...camas];

    for (var i = 0; i < newArray.length; i++) {
      if (newArray[i].globalIndex === globalIndex) {
        newArray[i].value = value;
        break;
      }
    }

    setCamas(newArray);
  }

  function setTipoCama(index: number, oldTipo: string, newTipo: string): void {
    var newArray = [...camas];

    for (var i = 0; i < newArray.length; i++) {
      if (newArray[i].index === index && newArray[i].tipo === oldTipo) {
        newArray[i].index = getNextCamaIndex(newArray, newTipo);
        newArray[i].tipo = newTipo;
        newArray[i].value = {};
        break;
      }
    }

    updateCamaIndexes(newArray);
    setCamas(newArray);
  }

  return (
    <ModalForm isVisible={isVisible} onHide={hide} onSubmit={onSubmit} resetOnChanged={resetOnChanged}>
      <Header title="Crear habitación" onHide={hide} />
      <Body>
        <ValidationSummary errors={validationErrors} />
        <Input label="Nombre" name="nombre" />

        <div className="columns">
          <div className="column">
            <Label text="Tipo" />
            <Select name="esPrivada">
              <option value="false">Compartida</option>
              <option value="true">Privada</option>
            </Select>
          </div>
          <div className="column">
            <SiNo name="tieneBanio" label="Baño privado" />
          </div>
          <div className="column is-two-fifths">
            <div className="field">
              <Label text="Información adicional" />
              <Textarea rows="3" name="informacionAdicional" placeholder="Ej.: tiene rampa para discapacitados" />
            </div>
          </div>
        </div>

        <div className="field">
          <Label text="Camas" />
          {camas.map(
            (cama): ReactElement => {
              return (
                <SelectCama
                  key={`${cama.globalIndex}`}
                  cama={cama}
                  setTipoCama={setTipoCama}
                  removeCama={removeCama}
                  setValue={setValue}
                />
              );
            }
          )}
          <Button text="Agregar cama" onClick={(): void => addCama()} style={{ marginTop: '1em' }} />
        </div>
      </Body>
      <FooterAcceptCancel onCancel={hide} loading={loading} />
    </ModalForm>
  );
};

export default Crear;
