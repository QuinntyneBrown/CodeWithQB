import { DashboardCard } from "../dashboard-cards/dashboard-card.model";

export class Dashboard {
  public dashboardId: string;
  public name: string;
  public dashboardCards: DashboardCard[] = [];
}
