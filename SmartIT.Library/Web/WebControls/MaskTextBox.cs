// <copyright file="MaskTextBox.cs" company="Eduardo Claudio Nicacio">
// Copyright Eduardo Claudio Nicacio. All rights reserved.
// </copyright>
// <author>Eduardo Claudio Nicacio</author>
// <date>03/07/2014</date>
// <summary>Caixa de texto com máscara.</summary>

namespace SmartIT.Library.Web.WebControls
{
    using System;
    using System.Web.Configuration;
    using System.Web.UI;
    using System.ComponentModel;

    /// <summary>
    /// Caixa de texto com máscara.
    /// </summary>
    public class MaskTextBox : System.Web.UI.WebControls.TextBox
    {
        private string jsPath;
        private readonly MaskTextBoxType mask = MaskTextBoxType.None;
        private readonly int decimalDigits = 0;
        private readonly string decimalSeparator = string.Empty;
        private readonly string groupSeparator = string.Empty;
        private readonly string validationMsg = string.Empty;

        /// <summary>
        /// Configuração inicial do controle.
        /// </summary>
        /// <param name="e"> Event Args.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            jsPath = WebConfigurationManager.AppSettings.Get("MaskTextBox-JavaScript-Path") ?? "~/JavaScript";
            RegisterScripts();
        }

        /// <summary>
        /// Ao final da tag gerada para o controle será gerado a configuração da máscara aplicada.
        /// </summary>
        /// <param name="writer"> Html Text Writer object.</param>
        public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "mask_" + this.ID, GetControlConfigurationScripts());
        }

        /// <summary>
        /// Inclue as referências para os scripts necessários.
        /// </summary>
        private void RegisterScripts()
        {
            // 1 - JavaScriptUtil.js
            Page.ClientScript.RegisterClientScriptInclude(GetType(), "JavaScriptUtil", Page.ResolveUrl(jsPath + "/JavaScriptUtil.js"));

            // 2 - Parsers.js
            Page.ClientScript.RegisterClientScriptInclude(GetType(), "Parsers", Page.ResolveUrl(jsPath + "/Parsers.js"));

            // 3 - InputMask.js
            Page.ClientScript.RegisterClientScriptInclude(GetType(), "InputMask", Page.ResolveUrl(jsPath + "/InputMask.js"));
        }

        /// <summary>
        /// Gera a configuração da máscara para o controle.
        /// </summary>
        /// <returns> Propriedade da máscara.</returns>
        private string GetControlConfigurationScripts()
        {
            string contentScript = string.Empty;

            switch (MaskType)
            {
                case MaskTextBoxType.Currency:

                    contentScript = ConfigCurrencyControl();
                    break;

                case MaskTextBoxType.CustomMask:

                    if (CustomMask == null)
                    {
                        throw new InvalidEnumArgumentException("A propriedade CustomMask deve ser definida quando o MaskType for CustomMask.");
                    }

                    contentScript = ConfigCustomControl();
                    break;

                case MaskTextBoxType.Date:

                    contentScript = ConfigDateControl();
                    break;

                case MaskTextBoxType.DateTime:

                    contentScript = ConfigDateTimeControl();
                    break;

                case MaskTextBoxType.Number:

                    contentScript = ConfigNumberControl();
                    break;

                default: break;
            }

            // retornar a configuracao
            return contentScript + "\n";
        }

        /// <summary>
        /// Gera a configuração de máscara númerica para valores monetários.
        /// </summary>
        /// <returns> Script gerado.</returns>
        private string ConfigCurrencyControl()
        {
            return "new NumberMask(new NumberParser(2, ',', '.', true, '', false), '" + UniqueID + "');";
        }

        /// <summary>
        /// Gera a configuração de máscara customizada na propriedade Mask do controle.
        /// </summary>
        /// <returns> Script gerado.</returns>
        private string ConfigCustomControl()
        {
            return "new InputMask('" + CustomMask + "', '" + UniqueID + "');";
        }

        /// <summary>
        /// Gera a configuração de máscara para data.
        /// </summary>
        /// <returns> Script gerado.</returns>
        private string ConfigDateControl()
        {
            if (validationMsg.Length == 0)
            {
                return "new DateMask('dd/MM/yyyy', '" + UniqueID + "');";
            }
            else
            {
                return string.Format("var dtMsk{0} = new DateMask('dd/MM/yyyy', '{1}'); dtMsk{2}.validationMessage = '{3}';",
                                    ClientID,
                                    UniqueID,
                                    ClientID,
                                    validationMsg.Replace("'", "''"));
            }
        }

        /// <summary>
        /// Gera a configuração de máscara para data e hora.
        /// </summary>
        /// <returns> Script gerado.</returns>
        private string ConfigDateTimeControl()
        {
            if (validationMsg.Length == 0)
            {
                return "new DateMask('dd/MM/yyyy HH:mm', '" + UniqueID + "');";
            }
            else
            {
                return string.Format("var dtMsk{0} = new DateMask('dd/MM/yyyy HH:mm', '{1}'); dtMsk{2}.validationMessage = '{3}';",
                                    ClientID,
                                    UniqueID,
                                    ClientID,
                                    validationMsg.Replace("'", "''"));
            }
        }

        /// <summary>
        /// Gera a configuração de máscara para data.
        /// </summary>
        /// <returns> Script gerado.</returns>
        private string ConfigNumberControl()
        {
            string parser = "new NumberParser(" + decimalDigits + ", '" + decimalSeparator.Trim() + "', '" + groupSeparator.Trim() + "', true, '', false)";
            string numberMask = "new NumberMask(" + parser + ", '" + UniqueID + "');";

            return numberMask;
        }

        /// <summary>
        /// Quando o tipo de mascara for Date irá retornar o Text convertido para DateTime e quando a máscara for Currency irá retornar o Text convertido para Decimal.
        /// </summary>
        public object Value
        {
            get
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    if (mask == MaskTextBoxType.Date || mask == MaskTextBoxType.DateTime)
                    {
                        return DateTime.Parse(Text);
                    }

                    if (mask == MaskTextBoxType.Currency)
                    {
                        return decimal.Parse(Text.Replace(".", string.Empty));
                    }

                    if (mask == MaskTextBoxType.Number)
                    {
                        return int.Parse(Text);
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Tipo da máscara que o controle irá exibir.
        /// </summary>
        [System.ComponentModel.DefaultValue("None")]
        public MaskTextBoxType MaskType { get; set; }

        /// <summary>
        /// Máscara customizada que o controle deverá assumir. A customização deverá obedecer os seguintes padrões.
        /// #, 0 ou 9 - Números
        /// a ou A - Letras
        /// ? ou _ - Qualquer caracter
        /// l ou L - Caracteres em minúsculo
        /// u ou U - Caracteres em maiúsculo
        /// c ou C - Inicial em maiúsculo
        /// \\, \#, \0, ... - Escape para caracteres
        /// Qualquer outro caracter - será ignorado
        /// </summary>
        public string CustomMask
        {
            get
            {
                if (ViewState[ID + "_Mask"] != null)
                {
                    return (string)ViewState[ID + "_Mask"];
                }
                else
                {
                    return null;
                }
            }

            set { ViewState[ID + "_Mask"] = value; }
        }

        /// <summary>
        /// Quando o MaskType for Number, esta propriedade é opcional para definir a quantidade de casas decimais. O valor padrão é 0 (sem casas decimais).
        /// </summary>
        public int DecimalDigits { get; set; }

        /// <summary>
        /// Quando o MaskType for Number, esta propriedade é opcional para definir o caractere separador de casa decimal. O valor padrão é vazio (sem separador).
        /// </summary>
        public string DecimalSeparator { get; set; }

        /// <summary>
        /// Quando o MaskType for Number, esta propriedade é opcional para definir o caractere separador de milhar. O valor padrão é vazio (sem separador).
        /// </summary>
        public string GroupSeparator { get; set; }

        /// <summary>
        /// Mensagem de validação.
        /// </summary>
        public string ValidationMessage { get; set; }
    }

    /// <summary>
    /// Tipo de máscara para o controle.
    /// </summary>
    public enum MaskTextBoxType
    {
        /// <summary>
        /// Currency format.
        /// </summary>
        Currency,

        /// <summary>
        /// Date in format dd/mm/yyyy.
        /// </summary>
        Date,

        /// <summary>
        /// DateTime in format dd/mm/yyyy hh:mi:ss.
        /// </summary>
        DateTime,

        /// <summary>
        /// Numer format.
        /// </summary>
        Number,

        /// <summary>
        /// Custom mask.
        /// </summary>
        CustomMask,

        /// <summary>
        /// No format.
        /// </summary>
        None
    }
}