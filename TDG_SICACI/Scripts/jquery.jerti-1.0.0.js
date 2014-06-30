/*****************************************************************
JQUERY OFICIAL DEL FRAMEWORK JERTI 1.0.0
CREADO POR: JUAN MIGUEL PEREZ PINEDA
REVISION: JULIO - 2012
*****************************************************************/
(function ($) {

    /*****************************************************************
    HANDLER PARA EL ENVIO DE FORMULARIOS AL CONTROLADOR
    *****************************************************************/
    $.handlerSendFormToController = function (event) {
        event.preventDefault();

        

        /*Lo primero es recuperar el Form que se desea enviar al controlador*/
        var $form = $($(this).attr('data-jerti-form'));

        /*Llamamos a la funcion encargada de mandar los datos a travez del formulario*/
        $form.sendForm();
    }

    /*****************************************************************
    HANDLER PARA EL EVENTO 'ALWAYS' DE LA FX 'AJAX' DE JQUERY
    *****************************************************************/
    $.handlerAjaxDone = function (data) {
        if (data.success) {
            /*Primero verificamos si el resultado del controlador no es una Redireccion que tiene la maxima prioridad*/
            if (data.redirectURL)
                window.location = data.redirectURL;

            /*Mostramos la notificación correspondiente si se ha especificado*/
            if (data.notify)
                $.addNotificacion({
                    titulo: data.notify.Titulo,
                    msj: data.notify.Mensaje,
                    icono: data.notify.Icono,
                    permanente: data.notify.Permanente,
                    tiempo: data.notify.Tiempo
                });
        } else
            $.showModelStateErrors(data.modelErrors);
    }

    /*****************************************************************
    HANDLER PARA EL EVENTO 'ALWAYS' DE LA FX 'AJAX' DE JQUERY
    *****************************************************************/
    $.handlerAjaxAlways = function () {
        $.desbloquearUI();
    }

    /*****************************************************************
    HANDLER PARA EL EVENTO 'FAIL' DE LA FX 'AJAX' DE JQUERY
    *****************************************************************/
    $.handlerAjaxFail = function (jqXhr, textStatus, errorThrown) {
        /*Desbloquemos la UI por cualquier cosa*/
        $.desbloquearUI();

        /*Ahora leemos los datos para mostrar el msj de error*/
        var errorData = $.parseJSON(jqXhr.responseText);
        if (errorData.notify == undefined)
            $.addNotificacion({ titulo: '¡Se ha detectado un error!', msj: errorData.msg, icono: 'ERR_EXCEPTION', permanente: true });
        else {
            /*Mensaje de error personalizado desde el controlador*/
            if (errorData.notify.Show)
                $.addNotificacion({
                    titulo: errorData.notify.Titulo,
                    msj: errorData.notify.Mensaje,
                    icono: errorData.notify.Icono,
                    permanente: errorData.notify.Permanente,
                    tiempo: errorData.notify.Tiempo
                });
        }
    }

    /*****************************************************************
    HANDLER PARA RESTAURAR LA FILA ANTERIOR EN EL jqGRID CUANDO SE UTILIA 'INLINE NAVIGATOR'
    *****************************************************************/
    $.handlerjqGridOnSelectedRowForNotas = function (id) {
        var $self = $(this),
            lastRowSelect = $self.attr('data-jerti-last-row-select');       //Recuperamos la última fila seleccionada

        if (id && id !== lastRowSelect) {
            $self.restoreRow(lastRowSelect);
            $self.attr('data-jerti-last-row-select', id)                    //Guardamos el ID de la nueva fila seleccionada
        }

        /*Activamos el modo edicion de la fila*/
        $self.jqGrid('editRow', id, {
            keys: true,
            oneditfunc: $.handlerOnEditCellForNotas,
            aftersavefunc: $.handlerAfterSaveCellForNotas
        });
    }

    /*****************************************************************
    HANDLER DEL jqGRID NOTAS PARA PODER CAMBIAR DE NOTA AL PRESIONAR ENTER
    *****************************************************************/
    $.handlerOnEditCellForNotas = function (rowid) {
        var $row = $('#' + rowid),
            $input = $row.find('td > input').first();
        $input.select();        //Seleccionamos el texto
    }

    /*****************************************************************
   HANDLER DEL jqGRID NOTAS PARA PODER CAMBIAR DE NOTA AL PRESIONAR ENTER
   *****************************************************************/
    $.handlerAfterSaveCellForNotas = function (rowId) {
        var $self = $(this),
            name = '#' + rowId,
            $nextRow = $(name).next();

        /*Vericamos que tengamos el ID de la siguiente columna*/
        if ($nextRow.lenght != 0) $nextRow.click();
    }

    /*****************************************************************
    HANDLER PARA EL ENVIO DE FORMULARIOS AL CONTROLADOR
    *****************************************************************/
    $.handlerFakejqGrid = function () {
    }

    /*****************************************************************
    HANDLER PARA CONSTRUIR EN EL JQGRID, UN 'COMBOBOX' CON SUS ITEMS
    *****************************************************************/
    $.buildSelectjqGrid = function (data) {
        var $select = '<select>',
            response = jQuery.parseJSON(data);

        $.each(response.Items, function (e, k) {
            $select += '<option value="' + k.Value + '" data-extra="' + k.Extra + '">' + k.Text + '</option>';
        });
        return $select + '</select>';
    };

    /*****************************************************************
    FX ENCARGADA DE MANDAR LOS DATOS DEL FORMULARIO
    *****************************************************************/
    $.fn.isValid = function () {
        var $self = this;

        if ($self.valid() == true)
            return true;
        else {
            $.addNotificacion({ titulo: 'Formulario Incompleto', msj: 'No se puede continuar debido a que algunos campos del formulario poseen errores. Por favor, corrija estos errores y vuelva a intentarlo.', icono: 'FORM_INCOMPLETE' });
            return false;
        }
    }

    /*****************************************************************
    FX ENCARGADA DE MANDAR LOS DATOS DEL FORMULARIO
    *****************************************************************/
    $.fn.sendForm = function (sendP) {
        var $self = this;

        /*Establecemos los valores por defecto*/
        sendP_def = {
            beforeEnviar: '',               //Evento antes de mandar los datos al servidor y los datos sean validos
            success: '',                    //Evento cuando se tiene una respuesta por parte del servidor
            extraParam: '',                 //Parametro para enviar algun dato extra en el string hacia el servidor
            overwriteURL: '',               //Sobreescribir la URL a donde se mandaran los datos al servidor
            bloquearUI: true,               //Determina si se bloquea la UI para enviar los datos o no
            overwriteData: '',
            contentType: 'application/x-www-form-urlencoded'
        }
        enviarParametros = jQuery.extend(sendP_def, sendP);

        /*Primero, verificamos si tenemos q reescribir la URL por la URL pasada por el usuario*/
        if (enviarParametros.overwriteURL == '') enviarParametros.overwriteURL = $self.attr('action');

        /*Verificamos si tenemos q reescribir los datos que se van enviar por los pasados por el usuario*/
        if (enviarParametros.overwriteData == '') enviarParametros.overwriteData = $self.serialize() + enviarParametros.extraParam;

        /*Provocamos el evento para ver si se ha definido una funcion para establecer los parametros*/
        var x = $self.triggerHandler('setParametros');
        if (x) enviarParametros.overwriteData = x;

        /*Ahora, validamos que los datos del formulario sean completamente validos*/
        if ($self.valid() == true) {
            /*Provocamos el evento de 'beforeEnviar', por si el usuario quiere proseguir o no con el envio de los datos*/
            if (enviarParametros.beforeEnviar != '') {
                var resBE = enviarParametros.beforeEnviar();
                /*Si el usuario regresa un FALSE, entonces cancelamos el envio*/
                if (!resBE) return;
            }

            /*Ahora verificamos si bloqueamos la UI o no dependiendo del parametro*/
            if (enviarParametros.bloquearUI) $.bloquearUI();

            /*Si ya tenemos todo listo, entonces procedemos a enviar los datos al lado del servidor*/
            $.ajax({
                url: enviarParametros.overwriteURL, type: "POST", data: enviarParametros.overwriteData, dataType: 'json',
                contentType: enviarParametros.contentType
            })
                .done(function (data) {
                    if (data.success) {
                        if (enviarParametros.success != '') enviarParametros.success(data.success, data.ID);
                        $self.trigger('saveSuccess', [data]);
                    }
                    $.handlerAjaxDone(data);        /*Llamamos al Handler por defecto*/
                })
                .fail($.handlerAjaxFail)
                .always($.handlerAjaxAlways)
        } else
            $.addNotificacion({ titulo: 'Formulario Incompleto', msj: 'No se puede continuar debido a que algunos campos del formulario poseen errores. Por favor, corrija estos errores y vuelva a intentarlo.', icono: 'FORM_INCOMPLETE' });
    }

    /*****************************************************************
    FX ENCARGADA DE MANDAR LOS DATOS DEL FORMULARIO
    *****************************************************************/
    $.fn.loadPartialView = function (loadPW) {
        var $self = this,
            $idLoading = $($self.attr('data-jerti-loading'));

        //Establecemos los valores por defecto
        loadPW_def = {
            bloqueoUI: false,                //Determina si se bloquea la UI cuando se van a traer los datos
            loadComplete: '',                //Evento una vez cargada la vista parcial sobre el DIV
            parametros: ''
        }
        var loadPWParam = jQuery.extend(loadPW_def, loadPW);

        /*Si no se pasaron parametros por la definicion de la FX, provocamos un evento para ver si necesita pasar parametros el usuario*/
        if (loadPWParam.parametros == '') {
            var x = $self.triggerHandler('setParametros');
            if (x) loadPWParam.parametros = x;
        }

        /*Ahora verificamos si bloqueamos la UI o no dependiendo del parametro*/
        if (loadPWParam.bloqueoUI) $.bloquearUI();
        else /*Como no se bloquea la UI, entonces verificamos si hay definido el atributo de "LOADING"*/
            if ($idLoading) {
                $self.hide()
                $idLoading.show();
            }
        
        //Cargamos por Ajax la Vista Parcial
        $self.load($self.attr('data-jerti-partialview'), loadPWParam.parametros, function (responseText, textStatus, XMLHttpRequest) {
            /*Desbloqueamos la UI u Ocultamos la animcacion de carga*/
            if (loadPWParam.bloqueoUI) $.desbloquearUI();
            else {
                $idLoading.hide();
                $self.show();
            }
            /*Verificamos si existe un error o no*/
            if (textStatus == 'success') {
                /*Provocamos el evento de LoadComplete si ha sido definido o disparamos el trigger sobre el elemento que cargo el HTML*/
                if (loadPWParam.loadComplete != '') loadPWParam.loadComplete();
                $self.trigger('loadSuccess', [XMLHttpRequest]);
            }
            else 
                $.handlerAjaxFail(XMLHttpRequest, textStatus)
        });
    }

    /*****************************************************************
    FX ENCARGADA DE MANDAR LOS DATOS DEL FORMULARIO
    *****************************************************************/
    $.fn.dialogPartialView = function () {
        var $self = this;
        /*Debemos de crear el handler para capturar el evento clic del control y poder mostrar el dialog*/
        $self.on('click', function (event) {
            event.preventDefault();
            var $trigger = $(this);

            /*Provocamos el evento de 'beforeOpen', Si el usuario desea cancelar mostrar el cuadro de dialogo*/
            var cancelOpen = $trigger.triggerHandler('beforeOpen');
            if (cancelOpen == false) return;
            
            /*Instanciamos las etiquetas que vamos a utilizar*/
            var $div = $($trigger.attr('data-jerti-dlg-container'));
            var $dlg = $($div.attr('data-jerti-dlg-id'));

            /*Verificamos si ya fue cargado por primera vez o no*/
            if ($dlg.length == 0) {
                $.bloquearUI();

                /*Ahora nos preparamos a cargar la vista parcial, haremos la peticion al servidor de la vista parcial*/
                $div.load($trigger.attr('href'), '', function (responseText, textStatus, XMLHttpRequest) {
                    $.desbloquearUI();      /*Desbloqueamos la UI*/

                    if (textStatus == 'success') {
                        var $divCont = $(this),
                            dlgName = $divCont.attr('data-jerti-dlg-id'),
                            paramDialog = $divCont.attr('data-jerti-dlg-parameters'),
                            formName = $divCont.attr('data-jerti-dlg-form');

                        /*Le establecemos el nombre al primer DIV devuelto en la vista parcial*/
                        $divCont.children().first().attr('id', dlgName.substring(1));

                        /*Establecemos el nombre del DIV Contenedor, al div que posee el DLG (ESTO SERVIRA CUANDO SE GUARDEN LOS DATOS)*/
                        var $dlgCargado = $($divCont.attr('data-jerti-dlg-id'));
                        $dlgCargado.attr('data-jerti-div-container', $divCont.attr('id'));

                        /*Establecemos el nombre al Formulario y activamos la validacion del BUG de las vistas parciales*/
                        $dlgCargado.find('form').first().attr('id', formName);
                        $.validator.unobtrusive.parseDynamicContent('#' + formName);

                        /*Creamos el Handler del sendForm para ver si sera necesario pasar parametros a travez del evento*/
                        $dlgCargado.find('form').first().on('setParametros', function () {
                            var $dlg = $(this).parent(),
                                divName = '#' + $dlg.attr('data-jerti-div-container'),
                                x = $($(divName).attr('data-jerti-dlg-trigger')).triggerHandler('setParametros');
                            if (x) return x;
                        });

                        /*Preparamos los parametros definidos por el usuario*/
                        var jQueryUIDialog_user = $.serializeStringJF(paramDialog);

                        /*Preparamos las opciones por defecto del jQueryDialog*/
                        jQueryUIDialog_def = {
                            modal: true,
                            title: $divCont.attr('data-jerti-dlg-titulo'),
                            autoOpen: true,
                            resizable: false,
                            height: 'auto',
                            width: 'auto',
                            buttons: {
                                Guardar: function () {
                                    var $dialog = $(this);
                                    var $form = $dialog.find('form').first();
                                    $form.sendForm({
                                        success: function (exitoso, ID) {
                                            if (exitoso) {
                                                var divName = '#' + $dialog.attr('data-jerti-div-container');
                                                $dialog.dialog('close');
                                                $($(divName).attr('data-jerti-dlg-trigger')).trigger('saveSuccess', [ID]);
                                            }
                                        }
                                    });
                                },
                                Cancelar: function () {
                                    $(this).dialog('close');
                                    $($divCont.attr('data-jerti-dlg-trigger')).trigger('cancelDialog'); /*Provocamos el evento*/
                                }
                            }
                        }

                        /*Unimas las propiedades por defecto y las propiedades del usuario*/
                        var UIDialogParam = jQuery.extend(jQueryUIDialog_def, jQueryUIDialog_user);

                        /*Provocamos los eventos de 'firstOpen' y 'open' para logica de terceros*/
                        $($divCont.attr('data-jerti-dlg-trigger')).trigger('firstOpen', [$dlgCargado.find('form').first()]);
                        $($divCont.attr('data-jerti-dlg-trigger')).trigger('open', [$dlgCargado.find('form').first()]);

                        /*Mostramos el cuadro de dialogo con todo los parametros establecidos*/
                        $dlgCargado.dialog(UIDialogParam);
                    }
                    else
                        $.handlerAjaxFail(XMLHttpRequest, textStatus)
                });
            } else {    //Si el dialog ya fue cargado una vez
                /*Si el formulario esta en modo "Add", entonces limpiaremos los errore y la informacion antes de mostrar el form*/
                if ($div.attr('data-jerti-dlg-add') == '1') {
                    $dlg.find('form input').val('').removeClass('input-validation-error');
                    $dlg.find('form span.field-validation-error').addClass('field-validation-valid').removeClass('field-validation-error');
                }

                $($div.attr('data-jerti-dlg-trigger')).trigger('open', [$dlg.find('form').first()]);
                $dlg.dialog('open');
            }
        });
    }


    /*****************************************************************
    FX ENCARGADA DE CARGAR POR MEDIO DE AJAX LOS DATOS DE UN COMBOBOX
    *****************************************************************/
    $.fn.loadComboBox = function (loadCB) {
        var $self = $(this),
            $idLoading = $($self.attr('data-jerti-loading'));

        //Establecemos los valores por defecto
        loadCB_def = {
            loadComplete: '',                //Evento una vez cargada la vista parcial sobre el DIV
            parametros: '',
            firstItem: '',
            bloquearScreen: false,
            bloquearScreenMSG: ''
        }
        var loadCBParam = jQuery.extend(loadCB_def, loadCB);

        /*Vaciamos y bloqueamos el control*/
        $self.empty();
        $self.attr('disabled', 'disabled');
        
        /*Si no se pasaron parametros por la definicion de la FX, provocamos un evento para ver si necesita pasar parametros el usuario*/
        if (loadCBParam.parametros == '') {
            var x = $self.triggerHandler('setParametros');
            if (x) loadCBParam.parametros = x;
        }

        if ($idLoading) $idLoading.show();
        
        /*Hacemos la peticion de los datos al controlador del Servidor*/
        $.ajax({ url: $self.attr('data-jerti-loadItems'), type: "POST", data: loadCBParam.parametros })
            .done(function (data) {
                /*Antes de empezar a agregar, verificamos que no se haya definido el primer item que sea un indicador para seleccionar*/
                if (loadCBParam.firstItem != '') $self.append('<option value="">' + loadCBParam.firstItem + '</option>');

                //Ahora agregamos nuevamente cada uno de los items al listado de proveedores
                $.each(data.Items, function (e, k) {
                    $self.append('<option value="' + k.Value + '" data-extra="' + k.Extra + '">' + k.Text + '</option>');
                });

                /*Si se ha pasado un algun item para seleccionar desde el controlador lo seleccionamos*/
                if (data.lastItem != '') $self.val(data.lastItem);

                //Habilitamos nuevamente el control
                $self.removeAttr('disabled');

                /*Provocamos un evento luego de haber cargado satisfactoriamente los datos*/
                if (loadCBParam.loadComplete != '') loadCBParam.loadComplete(data);
                $self.trigger('loadSuccess')
            })
            .fail(function (jqXhr, textStatus, errorThrown) {
                if ($idLoading) $idLoading.hide();  //Detenemos la animacion de carga
                /*Ahora le pasamos los datos la manejador de errores*/
                $.handlerAjaxFail(jqXhr, textStatus, errorThrown);
            })
            .always(function () {
                if ($idLoading) $idLoading.hide();  //Detenemos la animacion de carga
            });
    }


    /*****************************************************************
    FX ENCARGADA DE CARGAR UNA FUENTE DE DATOS QUE SIRVA DE CACHE PARA UN CAMPO AUTOCOMPLEMENTADO
    *****************************************************************/
    $.fn.loadArrayAutoComplete = function (loadAAC) {
        var $self = $(this),
            $idLoading = $($self.attr('data-jerti-loading')),
            $input = $($self.attr('data-jerti-input'));

        //Establecemos los valores por defecto
        loadAAC_def = {
            loadComplete: '',                //Evento una vez cargada la vista parcial sobre el DIV
            parametros: ''
        }
        var loadAACParam = jQuery.extend(loadAAC_def, loadAAC);

        /*Si no se pasaron parametros por la definicion de la FX, provocamos un evento para ver si necesita pasar parametros el usuario*/
        if (loadAACParam.parametros == '') {
            var x = $self.triggerHandler('setParametros');
            if (x) loadAACParam.parametros = x;
        }

        /*Si existe animacion de carga, la mostramos*/
        if ($idLoading) $idLoading.show();

        /*Bloqueamos el control que servira como Input*/
        if ($input) $input.attr('disabled', 'disabled');

        /*Hacemos la peticion de los datos al controlador del Servidor*/
        $.ajax({ url: $self.attr('data-jerti-loadItems'), type: "POST", data: loadAACParam.parametros })
            .done(function (data) {
                /*Una vez con los datos cargados, anexamos los datos al control INPUT del AutoComplete para que sirva de source al control*/
                $input.autocompletadoWithCache({ data: data.Items, minParam: $self.attr('data-jerti-minparam') });

                /*Removemos el bloqueo del control de input*/
                if ($input) $input.removeAttr('disabled');

                /*Provocamos un evento luego de haber cargado satisfactoriamente los datos*/
                if (loadAACParam.loadComplete != '') loadAACParam.loadComplete(data);
                $self.trigger('loadSuccess', [data])
            })
            .fail(function (jqXhr, textStatus, errorThrown) {
                if ($idLoading) $idLoading.hide();                          //Detenemos la animacion de carga
                $.handlerAjaxFail(jqXhr, textStatus, errorThrown);          /*Ahora le pasamos los datos la manejador de errores*/
            })
            .always(function () {
                if ($idLoading) $idLoading.hide();                          //Detenemos la animacion de carga
            });
    }

    /*****************************************************************
    FX ENCARGADA PARA ESTABLECER UN BOTON POR DEFAULT EN UNA PAGINA
    *****************************************************************/
    $.fn.defaultButton = function (btnOpt) {
        var $self = this, $tag;
        btnOpt_def = {          //Establecemos los valores por defecto
            form: 'form',
            element: ''
        }
        var btnOptParam = jQuery.extend(btnOpt_def, btnOpt);
        
        /*Verificamos si hemos establecido para ciertos elementos especificamente*/
        $tag = (btnOptParam.element == '' ? $(btnOptParam.form).find('input') : $tag = $(btnOptParam.element));
        /*Anexamos el handler encargado de establecer el boton por default*/
        $tag.on('keydown', function (event) {
            // Analizamos el tipo de tecla emitida
            var keycode = (event.keyCode ? event.keyCode : (event.which ? event.which : event.charCode));
            if (keycode == 13) {
                $self.click();                      // Forzamos el evento de click del control
                return false;
            } else 
                return true;
        });

    };


    /*****************************************************************
    FX PARA MOSTRAR LOS MSJ DE ERROR DEL MODELSTATE DE UN FORM
    *****************************************************************/
    $.showModelStateErrors = function (Errors) {
        //Debemos de recorrer uno por uno la coleccion de errores devuelto por el controlador
        $.each(Errors, function (e, k) {
            //Primero, establecemos al objeto del error con el estado visual de "ERROR"
            var $objInput = $('input[name="' + k.ID_Object + '"]');
            $objInput.removeClass('valid');
            $objInput.addClass('input-validation-error');

            //Segundo, debemos localizar la etiqueta "SPAN" que corresponde donde colocaremos el msj de error
            var $objErrSpan = $objInput.parent().find('span').first();
            $objErrSpan.text('');
            $objErrSpan.removeClass('field-validation-valid');
            $objErrSpan.addClass('field-validation-error');
            $objErrSpan.append('<span for="' + k.ID_Object + '" generated="true" class="" style="">' + k.MSG_Error + '</span>');
        });
        /*Mostramos la notificacion asociada a los errores encontrados en el formulario*/
        $.addNotificacion({ titulo: 'Formulario Incompleto', msj: 'No se puede continuar debido a que algunos campos del formulario poseen errores. Por favor, corrija estos errores y vuelva a intentarlo.', icono: 'FORM_INCOMPLETE' });
    };

    /*****************************************************************
    FX PARA MOSTRAR UNA NOTIFICACIÓN EN EL SISTEMA
    *****************************************************************/
    $.addNotificacion = function (notys_opt) {
        /*Establecemos los valores por defecto*/
        notysOpt_def = {
            titulo: 'Notificación de SchoolClick®',
            msj: '',
            icono: '',
            tiempo: 10000,
            permanente: false
        }
        var notysParametros = jQuery.extend(notysOpt_def, notys_opt);

        /*Le pasamos todos los parametros necesarias para mostrar la notificacion*/
        $.gritter.add({
            title: notysParametros.titulo,
            text: notysParametros.msj,
            time: notysParametros.tiempo,
            sticky: notysParametros.permanente,
            image: getIconNotificacion(notysParametros.icono)
        });
    }

    /*****************************************************************
    FX PRIVADA PARA DETERMINAR EL ICONO DE LA NOTIFICACION
    *****************************************************************/
    function getIconNotificacion(icono) {
        switch (icono) {
            case 'FORM_INCOMPLETE':
                return '../../../Content/images/notifications/notify_form_incomplete.png';
                break
            case 'ERR_EXCEPTION':
                return '../../../Content/images/notifications/notity_exception.png';
                break
            case 'SEND':
                return '../../../Content/images/notifications/notify_send.png';
                break
            case 'UPDATE':
                return '../../../Content/images/notifications/notify_update.png';
                break
            case 'NEW_DOC':
                return '../../../Content/images/notifications/notify_new_doc.png';
                break
            case 'DELETE':
                return '../../../Content/images/notifications/notify_delete.png';
                break
            case 'LINK':
                return '../../../Content/images/notifications/notify_link.png';
                break
            case 'IMPORT':
                return '../../../Content/images/notifications/notify_import.png';
                break
            case 'OPEN':
                return '../../../Content/images/notifications/notify_open.png';
                break
        }
    }

    /*****************************************************************
    FX ENCARGADA DE BLOQUEAR LA UI
    *****************************************************************/
    $.bloquearUI = function (buiMSG) {
        if (buiMSG == undefined) buiMSG = 'Por favor, espere un momento...';
        $.blockUI({
            message: buiMSG,
            css: {
                border: 'none', padding: '15px', backgroundColor: '#000', '-webkit-border-radius': '10px', '-moz-border-radius': '10px',
                'border-radius': '10px', opacity: .6, color: 'white'
            }
        });
    };

    /*****************************************************************
    FX ENCARGADA DE BLOQUEAR LA UI
    *****************************************************************/
    $.desbloquearUI = function () {
        $.unblockUI();
    };

    /*****************************************************************
    FX PARA LA CONVERSION DE STRING DE JERTI A JSON DOT
    *****************************************************************/
    $.serializeStringJF = function (cadena) {
        var properties = cadena.split(',');
        var obj = {};
        properties.forEach(function (property) {
            var tup = property.split(':');
            obj[tup[0]] = tup[1];
        });
        return obj;
    };

    /*****************************************************************
    FX PARA LA CONVERSION DE FORMULARIO A JSON .DOT
    *****************************************************************/
    $.fn.serializeObject = function (prefix) {
        prefix = (prefix == undefined ? '' : prefix);
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            var name = this.name.replace(prefix, '');

            if (o[name]) {
                if (!o[name].push) {
                    o[name] = [o[this.name]];
                }
                o[name].push(this.value || '');
            } else {
                o[name] = this.value || '';
            }
        });
        return o;
    };
})(jQuery);