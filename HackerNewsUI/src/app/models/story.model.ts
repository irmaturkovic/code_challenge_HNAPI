export interface IStory 
  {
    by: string;
    id: number;
    type: string;
    time: number;
    title: string;
    score: number;
    url?: string;
    text?: string;
  }