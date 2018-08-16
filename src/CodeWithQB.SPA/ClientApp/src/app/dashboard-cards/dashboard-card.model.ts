export class Options {
  public top: number;
  public left: number;
  public height: number;
  public width: number;
}

export class DashboardCard {
  public dashboardCardId: string;
  public name: string;
  public cardId: string;
  public dashboardId: string;
  public options: Options = new Options();
}
