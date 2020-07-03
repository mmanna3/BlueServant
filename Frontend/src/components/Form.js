import React, { useEffect } from "react";
import { useForm } from "react-hook-form";

export default function Form({ defaultValues, children, onSubmit, resetOnChanged }) {
  const { handleSubmit, register, reset } = useForm({ defaultValues });

  useEffect(() => {
    reset();
  }, [resetOnChanged, reset]);

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      {processContent(children)}
    </form>
  );

  function processContent(contenido) {
    if (Array.isArray(contenido)) {
      return processContentAsArray(contenido);
    } else
      return ProcessHtmlTree(contenido);
  }

  function processContentAsArray(contenido){
    return contenido.map((element,index) => {
      return ProcessHtmlTree(element, index);
    });
  }

  function ProcessHtmlTree(e, index, innerIndex) {    
    
    if (hasNameProperty(e))
      return addRegisterAttribute(e);
    
    if (isReactComponent(e)){    
      e = instantiate(e);
    }

    if (e == null)
      return e;

    if (hasNoChildren(e))
      return addRegisterAttribute(e);

    var children = getChildren(e);

    var newChildren = children.map((child, innerIndex) => {
      return ProcessHtmlTree(child, index, innerIndex);
    })
    
    return React.cloneElement(e, {...{...e.props, key: `${index}-${innerIndex}`}}, newChildren);      
  }

  function hasNameProperty(e){
    return e.props && e.props.name;
  }

  function hasNoChildren(e) {
    return !e.props || !e.props.children
  }

  function getChildren(a) {
    var children = [];
    if (Array.isArray(a.props.children))
      children = a.props.children;
    else {
      children.push(a.props.children);
    }
    return children;
  }

  function addRegisterAttribute(e){
    if (e.props && e.props.name)
      return React.createElement(e.type, {
        ...{
          ...e.props,
          register: register(),
          key: e.props.name
        }
      });
    else
      return e;
  }
}

function instantiate(e) {
  return e.type(e.props);
}

function isReactComponent(e) {
  return typeof e.type === 'function';
}
