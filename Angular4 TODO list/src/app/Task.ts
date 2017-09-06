export class Task{
    id: number;
    title: string;
    isComplete: boolean;
    removed: boolean;
    

    constructor(title:string) {
        this.title=title;
    }
        
};