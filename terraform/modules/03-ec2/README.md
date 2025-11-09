## Providers

| Name | Version |
| :-- | :-- |
| terraform | >= 1.5.7 |
| aws | >= 6.0 |

## Resources

| Name | Type |
|------|------|
| [aws_instance](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/instance) | resource |
| [aws_iam_role](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/iam_role) | resource |
| [aws_iam_instance_profile](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/iam_instance_profile) | resource |
| [aws_iam_role_policy_attachment](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/resources/iam_role_policy_attachment) | resource |
| [aws_iam_policy_document](https://registry.terraform.io/providers/hashicorp/aws/latest/docs/data-sources/iam_policy_document) | data source |

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_ec2_ami"></a> [ami](#input\_ami) | ID of AMI to use for the instance | `string` | n/a | yes |
| <a name="input_ec2_associate_public_ip_address"></a> [associate\_public\_ip\_address](#input\_associate\_public\_ip\_address) | Whether to associate a public IP address with an instance in a VPC  | `bool` | null | no |
| <a name="input_ec2_availability_zone"></a> [availability\_zone](#input\_availability\_zone) | AZ to start the instance in | `string` | null | no |
| <a name="input_ec2_iam_instance_profile"></a> [iam\_instance\_profile](#input\_iam\_instance\_profile) | IAM Instance Profile to launch the instance with. Specified as the name of the Instance Profile | `string` | null | yes |
| <a name="input_ec2_instance_type"></a> [instance\_type](#input\_instance\_type) | The type of instance to start | `string` | t2.micro | yes |
| <a name="input_ec2_name"></a> [name](#input\_name) | Name to be used on EC2 instance created | `string` | null | no |
| <a name="input_ec2_sg_ids"></a> [security\_groups\_ids](#input\_security\_groups\_ids) | Security Groups ids from Security Group module | `list(string)` | empty | yes |
| <a name="input_ec2_subnet"></a> [subnet](#input\_subnet) | Subnet id from the VPC module | `string` | empty | yes |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_ec2_id"></a> [id](#output_id) | The ID of the instance |
| <a name="output_ec2_public_ip"></a> [public\_ip](#output\_public\_ip) | The EC2 public IP (if possible) |
| <a name="output_ec2_az"></a> [availability\_zone](#output\_availability\_zone) | The availability zone of the created instance |

## Usage
```
module "ec2" {
  for_each = toset(var.ec2_name_set)

  source   = "./modules/03-ec2"
  ami      = var.ami
  sgs      = each.key == "dotnet" ? [module.security_groups.web_backend_security_group_id] : [module.security_groups.web_ui_security_group_id]
  ec2_name = each.key
  #place dotnet(backend) to private subnet
  subnet                      = each.key == "dotnet" ? module.vpc.vpc_subnet_ids["subnet2"] : module.vpc.vpc_subnet_ids["subnet0"]
  instance_type               = var.instance_type
  create_iam_instance_profile = true
  iam_role_description        = "IAM role for EC2 instance"
  iam_role_policies           = var.iam_role_policies
  # Add web_ui_port и web_backend_port
  web_ui_port                 = var.web_ui_port
  web_backend_port            = var.web_backend_port
  port                        = each.key == "react" ? var.web_ui_port : (each.key == "dotnet" ? var.web_backend_port : (each.key == "angular" ? var.web_ui_port : 80))
  target_group_arn            = each.key == "angular" ? module.alb.web_ui_angular_target_group_arn : (each.key == "react" ? module.alb.web_ui_react_target_group_arn : null)
  associate_public_ip_address = each.key != "dotnet"

}

```

## Output Example
```
"angular" = {
    "availability_zone" = "us-east-1a"
    "id" = "i-000..."
    "public_ip" = "..."
  }
```
