import { IMonth } from "../Models/Entity/IMonth";


export const MonthsOfYear = ():IMonth[]=>{
return [
    {MonthID:'01',MonthName:'ENERO'},
    {MonthID:'02',MonthName:'FEBRERO'},
    {MonthID:'03',MonthName:'MARZO'},
    {MonthID:'04',MonthName:'ABRIL'},
    {MonthID:'05',MonthName:'MAYO'},
    {MonthID:'06',MonthName:'JUNIO'},
    {MonthID:'07',MonthName:'JULIO'},
    {MonthID:'08',MonthName:'AGOSTO'},
    {MonthID:'09',MonthName:'SEPTIEMBRE'},
    {MonthID:'10',MonthName:'OCTUBRE'},
    {MonthID:'11',MonthName:'NOVIEMBRE'},
    {MonthID:'12',MonthName:'DICIEMBRE'}
  ];
};


export const YearRange = (nextYearAdd:number,previousYearAdd:number):string[]=>{
    //generar un anio
    //determina cuantos anios agrego hacia adelante y atras
    let currentDate:Date = new Date();
    let currentYear:number = currentDate.getFullYear();
    //let nextYearAdd:number = 10;
    //let previousYearAdd:number =  5;
    let yearStart:number =currentYear + nextYearAdd;
    //let yearFinal:number = currentYear - previousYearAdd;
    //generar anios
    let yearData:string[] = [];
    for(let i:number=0;i<(nextYearAdd + previousYearAdd);i++){
      yearData.push((yearStart - i).toString());
    }
    return yearData;
    }
    