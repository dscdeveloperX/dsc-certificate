export const DateToString = (date:Date):string=>{
    let y =  date.getFullYear().toString();
    let m =  (date.getMonth()+1).toString().padStart(2,'0');
    let d =  date.getDate().toString().padStart(2,'0');
    return `${y}-${m}-${d}`;
  }

  export const StringToDate = (date:string):Date=>{
    let dArray = date.split('-');
    return new Date(parseInt(dArray[0]), parseInt(dArray[1])-1,parseInt(dArray[2]));
  }
