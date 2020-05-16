import { observable, action, computed } from "mobx";
import { createContext, SyntheticEvent } from "react";
import { IActivity } from "../models/activity";
import agent from "../api/agent";

class ActivityStore {
  @observable activities: IActivity[] = [];
  @observable selectedActivity: IActivity | undefined;
  @observable loadingInitial = false;
  @observable editMode = false;
  @observable submitting = false;
  @observable target = "";

  @computed get activitiesByDate() {
    return this.activities.sort(
      (a, b) => Date.parse(a.date) - Date.parse(b.date)
    );
  }

  @action loadActivities = async () => {
    this.loadingInitial = true;
    try {
      const activities = await agent.Activities.list();
      activities.forEach((activity) => {
        activity.date = activity.date.split(".")[0];
        this.activities.push(activity);
      });
    } catch (error) {
      console.log(error);
    }

    this.loadingInitial = false; //putting it at end rather than before
  };

  @action selectActivity = (id: string) => {
    this.selectedActivity = this.activities.find(
      (activity) => activity.id === id
    );
    this.editMode = false;
  };

  @action setEditMode = () => {
    this.editMode = true;
  };

  @action openCreateForm = () => {
    this.selectedActivity = undefined;
    this.editMode = true;
  };

  @action createActivity = async (activity: IActivity) => {
    this.submitting = true;
    try {
      await agent.Activities.create(activity);
      this.activities.push(activity);
      this.editMode = false;
    } catch (error) {
      console.log(error);
    }
    this.submitting = false;
  };

  @action editActivity = (activity: IActivity) => {
    this.submitting = true;
    agent.Activities.update(activity)
      .then(() => {
        this.loadActivities();
        this.selectActivity(activity.id);
        this.editMode = false;
      })
      .then(() => (this.editMode = false));
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
