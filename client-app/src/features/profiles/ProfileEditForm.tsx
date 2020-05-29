import React from "react";
import { Form as FinalForm, Field } from "react-final-form";
import { combineValidators, isRequired } from "revalidate";
import { IProfile } from "../../app/models/profile";
import { Form, Button } from "semantic-ui-react";
import TextAreaInput from "../../app/common/form/TextAreaInput";
import TextInput from "../../app/common/form/TextInput";
import { observer } from "mobx-react-lite";

const validate = combineValidators({
  displayName: isRequired("displayName"),
});

interface IProps {
  updateProfile: (profile: IProfile) => void;
  profile: IProfile;
  setEditMode: (editMode: boolean) => void;
}

const ProfileEditForm: React.FC<IProps> = ({
  updateProfile,
  profile,
  setEditMode,
}) => {
  return (
    <FinalForm
      onSubmit={updateProfile}
      validate={validate}
      initialValues={profile!}
      render={({ handleSubmit, invalid, pristine, submitting }) => {
        const handleSubmitAndMode = () => {
          handleSubmit();
          setEditMode(false);
        };

        return (
          <Form onSubmit={handleSubmitAndMode} error>
            <Field
              name="displayName"
              component={TextInput}
              placeholder="Display Name"
              value={profile!.displayName}
            />
            <Field
              name="bio"
              component={TextAreaInput}
              rows={3}
              placeholder="Bio"
              value={profile!.bio}
            />
            <Button
              loading={submitting}
              floating="right"
              disabled={invalid || pristine}
              positive
              content="Update profile"
            />
          </Form>
        );
      }}
    />
  );
};

export default observer(ProfileEditForm);
