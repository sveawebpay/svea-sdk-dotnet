export interface IThemeColors {
    transparent: string;
    aqua: string;
    turquoise: string;
    marine: string;
    orange: IColorVariants;
    green: IColorVariants;
    yellow: string;
    purple: string;
    red: IColorVariants;
    blue: IColorVariants;
    solnaWhite: IColorVariants;
    black: IColorVariants;
    buttons: ISveaButtonColors;
    alert: IAlertTypes;
    textButtons(variant: CardTheme): IButtonColors;
    avatar(variant: CardTheme): IPanelColors;
    cards(variant: CardTheme): ICardColors;
}
type CardTheme = 'lightblue' | 'white' | 'solnawhite1' | 'solnawhite2';
interface IColorVariants {
    [key: number]: string;
}

interface ICardColors {
    background: string;
}

interface IPanelColors {
    text: string;
    border: string;
    background: string;
}

interface IButtonColors {
    disabled: string;
    focus: string;
    hover: string;
    label: string;
    main: string;
    pressed: string;
    ripple: string;
}

interface ISveaButtonColors {
    primary: IButtonColors;
    secondary: IButtonColors;
}

interface IAlertProps {
    color: string;
    background: string;
    border: string;
}

interface IAlertTypes {
    error: IAlertProps;
    info: IAlertProps;
    success: IAlertProps;
    warning: IAlertProps;
    pending: IAlertProps;
}

export interface IThemeColors {
    transparent: string;
    aqua: string;
    turquoise: string;
    marine: string;
    orange: IColorVariants;
    green: IColorVariants;
    yellow: string;
    purple: string;
    red: IColorVariants;
    blue: IColorVariants;
    solnaWhite: IColorVariants;
    black: IColorVariants;
    buttons: ISveaButtonColors;
    alert: IAlertTypes;
    textButtons(variant: CardTheme): IButtonColors;
    avatar(variant: CardTheme): IPanelColors;
    cards(variant: CardTheme): ICardColors;
}