import { SortDirection } from "../shared/enums";

export class Pager {
    public constructor(init?: Partial<Pager>) {
        Object.assign(this, init);
    }

    selectItemsPerPage: number[] = [5, 10, 25, 50, 100];
    pageSize = 20;
    pageIndex: number = 1;
    allItemsLength: number = 0;
    sortBy: number;
    filterBy:number;
    filterText:string;
    sortDirection: number = SortDirection.Desc ;
}
