import { observable, action, computed } from "mobx";
import { createContext, SyntheticEvent } from "react";
import { IActivity } from "../models/activity";
import agent from "../api/agent";

class ActivityStore {
  @observable activityRegistry = new Map();

  @observable activities: IActivity[] = [];
  @observable selectedActivity: IActivity | undefined;
  @observable loadingInitial = false;
  @observable editMode = false;
  @observable submitting = false;
  @observable target = "";

  @computed get activitiesByDate() {
    return Array.from(this.activityRegistry.values()).sort(
      (a, b) => Date.parse(a.date) - Date.parse(b.date)
    );
  }

  @action loadActivities = async () => {
    this.loadingInitial = true;
    try {
      const activities = await agent.Activities.list();
      activities.forEach((activity) => {
        activity.date = activity.date.split(".")[0];
        this.activityRegistry.set(activity.id, activity);
      });
    } catch (error) {
      console.log(error);
    }

    this.loadingInitial = false; //putting it at end rather than before
  };

  @action selectActivity = (id: string) => {
    this.selectedActivity = this.activityRegistry.get(id);
    this.editMode = false;
  };

  @action openCreateForm = () => {
    this.selectedActivity = undefined;
    this.editMode = true;
  };

  @action openEditForm = (id: string) => {
    this.selectActivity = this.activityRegistry.get(id);
    this.editMode = true;
  };

  @action cancelSelectedActivity = () => {
    this.selectedActivity = undefined;
  };

  @action cancelFormOpen = () => {
    this.editMode = false;
  };

  @action createActivity = async (activity: IActivity) => {
    this.submitting = true;
    try {
      await agent.Activities.create(activity);
      this.activityRegistry.set(activity.id, activity);
      this.editMode = false;
    } catch (error) {
      console.log(error);
    }
    this.submitting = false;
  };

  @action editActivity = async (activity: IActivity) => {
    this.submitting = true;
    try {
      await agent.Activities.update(activity);
      this.activityRegistry.set(activity.id, activity);
      this.selectedActivity = activity;
      this.editMode = false;
    } catch (error) {
      console.log(error);
    }

    this.submitting = false;
  };

  @action deleteActivity = (
    event: SyntheticEvent<HTMLButtonElement>,
    id: string
  ) => {
    this.submitting = true;
    this.target = event.currentTarget.name;
    agent.Activities.delete(id)
      .then(() => {
        this.activities = this.activities.filter((a) => a.id !== id);
      })
      .then(() => (this.submitting = false));
  };
}

export default createContext(new ActivityStore());
